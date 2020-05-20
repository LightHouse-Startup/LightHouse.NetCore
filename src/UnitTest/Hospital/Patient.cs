using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Hospital
{
    public class Patient : INotifyPropertyChanged
    {
        public Patient()
        {
            IsNew = true;
            BloodSugar = 5.0f;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public int HeartBeatRate { get; set; }

        public bool IsNew { get; set; }

        public float BloodSugar { get; set; }

        public void IncreaseHeartBeatRate()
        {
            HeartBeatRate = CalculateHeartBeatRate() + 2;
        }

        private int CalculateHeartBeatRate()
        {
            var random = new Random();
            return random.Next(1, 100);
        }

        public void HaveDinner()
        {
            var random = new Random();
            BloodSugar += (float)random.Next(1, 1000) / 1000;
            OnPropertyChanged(nameof(BloodSugar));
        }

        public void Sleep()
        {
            OnPatientSlept();
        }

        public event EventHandler<EventArgs> PatientSlept;

        protected virtual void OnPatientSlept()
        {
            PatientSlept?.Invoke(this, EventArgs.Empty);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
