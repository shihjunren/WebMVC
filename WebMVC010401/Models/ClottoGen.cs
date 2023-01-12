using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMVC010401.Models
{
    public class ClottoGen
    {

        public string getnumbers()
        {
            Random x = new Random();
            int count = 0;
            int[] nu = new int[6];



            while (count < 6)
            {
                int temp = x.Next(1, 50);
                //bool isSelected= false;
                //for (int i =0;i<nu.Length;i++) 
                //{
                //    if (temp == nu[i])
                //    {
                //        isSelected = true;
                //        break;
                //    }
                //}
                if (!is亂數已經存在陣列中(temp,nu)) 
                { 
                    nu[count] = temp;
                    count++;
                }
            }

            for (int i = 0; i < nu.Length; i++)
            {
                for (int j = 0; j < nu.Length - 1; j++)
                {
                    if (nu[j] > nu[j + 1])
                    {
                        int big = nu[j];
                        nu[j] = nu[j + 1];
                        nu[j + 1] = big;
                    }

                }
            }

            string s = "";
            foreach (int i in nu)
                s += i.ToString() + " ";
            return s;


        }

        private bool is亂數已經存在陣列中(int temp, int[] nu)
        {
            for (int i=0;i<nu.Length;i++)
            {
                if (temp == nu[i])
                { return true; }
            }
            return false;
        }
    }
}