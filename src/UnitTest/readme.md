## 执行测试
1. “在Vs里找到“测试资源管理器”，运行测试
2. 在xunit项目下执行`dotnet test`

## 忽略测试
```csharp
[Fact(Skip = "不需要跑这个测试")]
```

## 测试分组

1. 在class或method上方使用`Trait`属性标注，eg：`[Trait("Category", "Worker")]`
1. 在Vs里找到“测试资源管理器”，“分组依据”按“特征”过滤，即可看到分组的测试用例
1. 只执行指定分组的测试 
```
dotnet test --filter "Category=Worker|Category=Plumber"
```

## 打印输出

`Xunit.Abstractions.ITestOutputHelper`

eg:

```csharp
public class WorkerShould
{
    private readonly ITestOutputHelper _output;

    public WorkerShould(ITestOutputHelper output)
    {
        _output = output;
    }
}
```

我习惯把他封装到基类中，让WorkerShould等去继承

## 测试报告
```
dotnet test --filter "Category=Worker" --logger:trx
```

## 减少重复代码
1. 复用创建代码

    每次执行被标记了Fact或Theory属性的方法时，都会重新创建相应的实例，可以把相同的对象创建移动到constructor方法里面

    这里每个Fact/Theory都是互相独立的，不用担心通过测试方法里对对象属性的变更会影响其他测试

    ```csharp
    private readonly WorkerFactory factory;

    public WorkerShould(ITestOutputHelper output) : base(output)
    {
        factory = new WorkerFactory();
        _output.WriteLine("正在创建新的...");
    }
    ```

2. 复用清理代码
    ```csharp
    public class TestBase : IDisposable
    {
        /// <summary>
        /// 用于打印输出
        /// </summary>
        public readonly ITestOutputHelper _output;

        public TestBase(ITestOutputHelper output)
        {
            _output = output;
        }

        /// <summary>
        /// 要主动标记为virtual才能被派生类重写
        /// </summary>
        public virtual void Dispose()
        {
            _output.WriteLine($"正在清理对象{this.GetType().FullName}");
        }
    }
    ```

## 共享上下文
如果对象创建比较耗时，上述在constructor里面简单创建的方式就不可取

1. 在一个测试类中不同的测试里共享

    参考代码如下，注意观察打印的id，确实只实例化了一次；fact方法里面的注释为之前的写法

    ```csharp
    /// <summary>
    /// 用于创建共享的Plumber实例
    /// </summary>
    public class PlumberFixture : IDisposable
    {
        public Plumber Instance { get; private set; }

        public PlumberFixture()
        {
            Instance = new Plumber();
        }

        public void Dispose()
        {
            // Cleanup
        }
    }

    [Trait("Category", "Plumber")]
    public class PlumberShould : TestBase, IClassFixture<PlumberFixture>
    {
        //private readonly Plumber plumber;
        private readonly PlumberFixture _plumberFixture;

        public PlumberShould(PlumberFixture plumberStateFixture, ITestOutputHelper output) : base(output)
        {
            //plumber = new Plumber();
            //_output.WriteLine($"正在创建新的...{plumber.Id}");

            _plumberFixture = plumberStateFixture;
            _output.WriteLine($"正在创建新的...{_plumberFixture.Instance.Id}");
        }

        [Fact]
        public void HaveCorrectSalary()
        {
            //var plumber = new Plumber();
            //Assert.Equal(66.667, plumber.Salary, 3);
            Assert.Equal(66.667, _plumberFixture.Instance.Salary, 3);
        }
    }
    ```

2. 在不同的测试类中共享上下文
       
    在测试类中通过`Collection`来获取共享实例

    ```csharp
    [CollectionDefinition("PlumberCollection")]
    public class PlumberCollection : ICollectionFixture<PlumberFixture>
    {
    }

    [Trait("Category", "Collection")]
    [Collection("PlumberCollection")]
    public class PlumberTest1 : TestBase
    {
        private readonly PlumberFixture _gameStateFixture;

        public PlumberTest1(PlumberFixture plumberFixture, ITestOutputHelper output) : base(output)
        {
            _gameStateFixture = plumberFixture;
        }

        [Fact]
        public void Test1()
        {
            _output.WriteLine($"Plumber ID={_gameStateFixture.Instance.Id}");
        }

        [Fact]
        public void Test2()
        {
            _output.WriteLine($"Plumber ID={_gameStateFixture.Instance.Id}");
        }
    }

    [Trait("Category", "Collection")]
    [Collection("PlumberCollection")]
    public class PlumberTest2 : TestBase
    {
        private readonly PlumberFixture _gameStateFixture;

        public PlumberTest2(PlumberFixture plumberFixture, ITestOutputHelper output) : base(output)
        {
            _gameStateFixture = plumberFixture;
        }

        [Fact]
        public void Test3()
        {
            _output.WriteLine($"Plumber ID={_gameStateFixture.Instance.Id}");
        }

        [Fact]
        public void Test4()
        {
            _output.WriteLine($"Plumber ID={_gameStateFixture.Instance.Id}");
        }
    }
    ```

## 数据驱动测试
1. InlineData
    ```csharp
    [Theory]
    [InlineData(200, 4, 50)]
    [InlineData(200, 3, 66.67)]
    public void CalculateSalary(double money, double hour, double expectSalary)
    {
        Assert.Equal(_plumberFixture.Instance.CalculateSalary(money, hour), expectSalary);
    }
    ```

2. MemberData
    ```csharp
    public class PlumberInternalTestData
    {
        private static readonly List<object[]> Data = new List<object[]>
        {
            new object[] {100, 3,33.33},
            new object[] {100, 4,25},
            new object[] {100, 2,50},
            new object[] {100, 6,16.67}
        };

        public static IEnumerable<object[]> TestData => Data;
    }

    [Theory]
    [MemberData(nameof(PlumberInternalTestData.TestData), MemberType = typeof(PlumberInternalTestData))]
    public void CalculateSalaryInternalTest(double money, double hour, double expectSalary)
    {
        Assert.Equal(_plumberFixture.Instance.CalculateSalary(money, hour), expectSalary);
    }
    ```

3. 外部数据
    ```csv
    200,3,66.67
    200,4,50
    200,5,40
    200,6,33.33
    ```
    以上为TestData.csv内容

    ```csharp
    public class PlumberExternalTestData
    {
        public static IEnumerable<object[]> TestData
        {
            get
            {
                string[] csvLines = File.ReadAllLines("TestData.csv");
                var testCases = new List<object[]>();
                foreach (var csvLine in csvLines)
                {
                    IEnumerable<double> values = csvLine.Split(',').Select(double.Parse);
                    object[] testCase = values.Cast<object>().ToArray();
                    testCases.Add(testCase);
                }
                return testCases;
            }
        }
    }

    [Theory]
    [MemberData(nameof(PlumberExternalTestData.TestData), MemberType = typeof(PlumberExternalTestData))]
    public void CalculateSalaryExternalTest(double money, double hour, double expectSalary)
    {
        Assert.Equal(_plumberFixture.Instance.CalculateSalary(money, hour), expectSalary);
    }
    ```

4. CustomDataAttribute

    ```csharp
    public class PlumberDataAttribute : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            yield return new object[] { 300, 100, 3 };
            yield return new object[] { 300, 2, 150 };
            yield return new object[] { 300, 50, 6 };
        }
    }

    [Theory]
    [PlumberData]
    public void CalculateSalaryDataAttribute(double money, double hour, double expectSalary)
    {
        Assert.Equal(_plumberFixture.Instance.CalculateSalary(money, hour), expectSalary);
    }
    ```

## 参考资料
* [xUnit](https://www.cnblogs.com/cgzl/p/9178672.html#test)
