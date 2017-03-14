using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
namespace Painter
{
    class FigSerialization
    {
        
        public void Serialize(string FileName,FigList Figures)
        {
            using (Stream FileStream = new FileStream(FileName, FileMode.Create,
                                                        FileAccess.Write, FileShare.None))
            {
                BinaryFormatter Formatter = new BinaryFormatter();
                Formatter.Serialize(FileStream, Figures);
            }
        }
        public FigList Deserialize(string FileName)
        {
            using (Stream FileStream = new FileStream(FileName, FileMode.Open,
                                                        FileAccess.Read, FileShare.None))
            {
                BinaryFormatter Formatter = new BinaryFormatter();
                return (FigList)Formatter.Deserialize(FileStream);
            }
        }   
    }
}
