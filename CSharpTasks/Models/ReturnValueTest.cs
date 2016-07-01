namespace CSharpTasks.Models
{
    public class ReturnValueTest
    {
        public string Name { get; set; }
        public double Number { get; set; }

        public override string ToString()
        {
            return $"Name: {this.Name} - Number: {this.Number}";
        }
    }
}
