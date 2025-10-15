using System.Text;

namespace Calculator.MyStuff
{
    internal class CalcParser
    {
        public static float Parse(string input)
        {
            float result = 0;

            string[] multiply = input.Split('*');

            string[] add = input.Split('+');

            string[][] subtract = new string[add.Length][];
            for (int i = 0; i < add.Length; i++)
            {
                subtract[i] = add[i].Split('-');
            }

            for ( int i = 0; i < subtract.Length; i++)
            {
                for ( int j = 1; j < subtract[i].Length; j++)
                {
                    subtract[i][j] = "-" + subtract[i][j];
                }
            }
            
            for ( int i = 0; i < subtract.Length; i++)
            {
                subtract[i] = [.. subtract[i].Where(s => !string.IsNullOrWhiteSpace(s))];
            }

            foreach (string[] tok in subtract)
            {
                foreach(string token in tok)
                {
                    result += float.Parse(token);
                }
            }

            return result;
        }

        public static float ParseTwo(string input)
        {
            float result = 0;

            if (input.Contains('*'))
            {
                string[] nums = new string[2];

                int index = input.IndexOf('*');

                nums[0] = FindSide(input, index, false);
                nums[1] = FindSide(input, index, true);

                result = float.Parse(nums[0]) * float.Parse(nums[1]);
            }

            return result;
        }

        public static string FindRightNumber(string input, int index)
        {
            StringBuilder sb = new();
            for (int i = index + 1; i < input.Length; i++)
            {
                if (IsOperator(input[i]))
                {
                    break;
                }
                sb.Append(input[i]);
            }
            return sb.ToString();
        }

        public static string FindLeftNumber(string input, int index)
        {
            StringBuilder sb = new();
            for (int i = index - 1; i >= 0; i--)
            {
                if (IsOperator(input[i]))
                {
                    break;
                }
                sb.Insert(0, input[i]);
            }
            return sb.ToString();
        }

        public static string FindSide(string input, int index, bool right)
        {
            int i = right ? index + 1 : index - 1;
            int end = right ? input.Length : -1;
            int step = right ? 1 : -1;


            StringBuilder sb = new();
            for (; i !=end; i += step)
            {
                if (IsOperator(input[i])) break;

                if (right) sb.Append(input[i]);
                else sb.Insert(0, input[i]);
            }
            return sb.ToString();
        }

        public static bool IsOperator(char c)
        {
            return c == '+' || c == '-' || c == '*' || c == '/';
        }
    }
}
