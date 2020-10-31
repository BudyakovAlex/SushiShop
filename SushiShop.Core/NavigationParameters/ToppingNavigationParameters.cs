﻿using System.Collections.Generic;
using SushiShop.Core.Data.Models.Toppings;

namespace SushiShop.Core.NavigationParameters
{
    public class ToppingNavigationParameters
    {
        public ToppingNavigationParameters(List<Topping> toppings) 
        {
            Toppings = toppings;
        }

        public List<Topping> Toppings { get; }
    }
}