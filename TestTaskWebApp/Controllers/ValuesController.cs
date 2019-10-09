using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestTaskWebApp.Models;
using TestTaskWebApp.NewFolder;

namespace TestTaskWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpPost("oddNumbersSum")]
        public ActionResult<long> OddNumbersSum([FromBody] OddNumbersSumModel model)
        {
            var oddNumbersCounter = 0;
            var sum = 0;
            foreach (var number in model.Array)
            {
                if (number % 2 != 0)
                {
                    sum = oddNumbersCounter == 1 ? sum + number : sum;
                    oddNumbersCounter = oddNumbersCounter == 0 ? oddNumbersCounter + 1 : 0;
                }
            }

            return Math.Abs(sum);
        }
    
        [HttpPost("longNumbersSum")]
        public ActionResult<LinkedList<int>> LongNumbersSum([FromBody] LongNumbersSumModel model)
        {
            var result = new LinkedList<int>();
            result.AddFirst(0);
            var firstEnumerator = model.First.First;
            var secondEnumerator = model.Second.First;
            var resultEnumerator = result.First;
            int remember = 0;
            while (firstEnumerator.Next != null || secondEnumerator.Next != null)
            {
                int sum = firstEnumerator.Value + secondEnumerator.Value + remember;
                var value = sum >= 10 ? sum - 10 : sum;
                remember = sum >= 10 ? 1 : 0;
                resultEnumerator.Value = value;
                result.AddAfter(resultEnumerator, 0);
                resultEnumerator = resultEnumerator.Next;
                if (firstEnumerator.Next == null)
                {
                    model.First.AddAfter(firstEnumerator, 0);
                }
                if (secondEnumerator.Next == null)
                {
                    model.Second.AddAfter(secondEnumerator, 0);
                }
                firstEnumerator = firstEnumerator.Next;
                secondEnumerator = secondEnumerator.Next;
            }

            int lastSum = firstEnumerator.Value + secondEnumerator.Value + remember;
            resultEnumerator.Value = lastSum % 10;
            if (lastSum >= 10)
            {
                result.AddAfter(resultEnumerator, 0);
                resultEnumerator = resultEnumerator.Next;
                resultEnumerator.Value = 1;
            }
            return result;
        }

        [HttpPost("palindrome")]
        public ActionResult<bool> Palindrome([FromBody] PalindromeModel model)
        {
            if (model.PalindromeOrNot == null)
            {
                return false;
            }

            bool isPalindrome = true;
            for (int i = 0; i < model.PalindromeOrNot.Length / 2; i++)
            {
                if (model.PalindromeOrNot[i] != model.PalindromeOrNot[model.PalindromeOrNot.Length - i - 1])
                {
                    isPalindrome = false;
                }
            }

            return isPalindrome;
        }
    }
}
