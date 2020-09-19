using Microsoft.AspNetCore.Components;
using PRIME_UCR.BusinessLogic.Interfaces;
using PRIME_UCR.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRIME_UCR.Pages
{
    public partial class TestPage
    {
        private TestModel myModel;

        [Inject]
        public ITestService MyService { get; set; }

        protected override void OnInitialized()
        {
            Random rand = new Random();
            myModel = MyService.CreateModel(rand.Next());
        }
    }
}
