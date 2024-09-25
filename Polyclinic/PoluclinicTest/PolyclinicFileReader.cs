using PolyclinicClasses;

namespace PoluclinicTest
{
    internal class PolyclinicFileReader(string fileName)
    {
        public List<Doctor> ReadDoctor()
        {
            var steamReader = new StreamReader(fileName);
            var doctors = new List<Doctor>();

            while (!steamReader.EndOfStream)
            {
                var line = steamReader.ReadLine();
            }
            return doctors;
        }
    }
}
