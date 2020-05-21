using gRPC.Server.Repository;
using Grpc.Core;
using Light.House.GRpc.Server.Protos;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Light.House.GRpc.Server.Services
{
    public class GrpcEmployeeService : EmployeeService.EmployeeServiceBase  //继承
    {
        /// <summary>
        /// 一元操作演示 —— 根据id获取员工数据
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<EmployeeResponse> GetEmployeeById(GetEmployeeByIdRequest request, ServerCallContext context)
        {
            //读取请求头中的元数据(应用层自定义的 key-value 对)
            var metaDataIdHeaders = context.RequestHeaders;
            foreach (var data in metaDataIdHeaders)
            {
                Console.WriteLine($"{data.Key} => {data.Value}");
            }

            //根据请求的Id找到员工信息
            var employee = EmployeeRepository.Emloyees.SingleOrDefault(emp => emp.Id == request.Id);

            if (employee == null) throw new RpcException(Status.DefaultSuccess, $"Employee of {request.Id} is not found");

            var response = new EmployeeResponse { Employee = employee };
            return await Task.FromResult(response);
        }
    }
}
