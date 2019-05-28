using System.ComponentModel;
using System.Threading.Tasks;

namespace TreeViewsExample
{
    public class Class1 : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
        private string _Test;

        public Class1()
        {
            Task.Run(async () =>
            {
                int i = 0;
                while (true)
                {
                    await Task.Delay(200);
                    Test = (i++).ToString();
                    
                }
            });
        }

        public string Test
        {
            get
            {
                return _Test;
            }
            set
            {
                if (_Test == value)
                    return;

                _Test = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Test)));
            }
        }

        public override string ToString()
        {
            return "Hola mundo";
        }
    }
}
