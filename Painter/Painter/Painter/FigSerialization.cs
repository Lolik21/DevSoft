using System.Runtime.Serialization.Json;
using System.IO;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;


namespace Painter
{
    class FigSerialization
    {
        public void Serialize(string FileName, FigList Figures, List<Type> Types)
        {
            using (Stream FileStream = new FileStream(FileName, FileMode.Create,
                                                        FileAccess.Write, FileShare.None))
            {
                DataContractJsonSerializer jsonFormatter = new
                    DataContractJsonSerializer(Figures.GetType(),Types);
                jsonFormatter.WriteObject(FileStream, Figures);
            }
        }
        public void Deserialize(string FileName,ref FigList Figures, List<Type> Types)
        {
            using (Stream FileStream = new FileStream(FileName, FileMode.Open,
                                                        FileAccess.Read, FileShare.None))
            {
                string str;
                using (StreamReader reader = new StreamReader(FileStream))
                {
                    str = reader.ReadLine();
                }
                Regex Pattern = new Regex("\\{\"__type\":\"(.+?):");

                foreach (Match match in Pattern.Matches(str))
                {
                    if (!IsInTypes(match.Groups[1].Value,Types))
                    {
                        DeleteType(match.Groups[0].Index,ref str);
                    }
                }

                byte[] buff = Encoding.Default.GetBytes(str);
                MemoryStream stream = new MemoryStream(buff);
                    DataContractJsonSerializer jsonFormatter = new
                        DataContractJsonSerializer(Figures.GetType(), Types);
                Figures = (FigList)jsonFormatter.ReadObject(stream);
            }
        }
        public bool IsInTypes(string str, List<Type> Types)
        {
            for (int i = 0; i<Types.Count; i++)
            {
                if (Types[i].Name == str) return true;
            }
            return false;
        }
        public void DeleteType(int ind,ref string str)
        {
            int FirstInd = ind;
            int LastInd = ind+1;
            bool IsFirst = false;
            if (str[FirstInd - 1] == ',')
            {
                FirstInd--;
                IsFirst = false;
            }
            else IsFirst = true;

            int Brackets = 1;
            int i = 0;
            for (i = ind+1; i<=str.Length && Brackets >= 1; i++)
            {
                if (str[i] == '{') Brackets++;
                if (str[i] == '}') Brackets--;
                LastInd++;
            }
            if (i < str.Length)
            {
                if (!IsFirst)
                {
                    LastInd--;
                }
                str = str.Remove(FirstInd, LastInd - FirstInd + 1);
            }

            

        }   
    }
}
