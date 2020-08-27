using OdeToFood.Core;
using System.Linq;
using System.Collections.Generic;


namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetAll();
    }

    public class InMemoryRestaurantData : IRestaurantData
    {
        readonly List<Restaurant> restaurants;

        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>()
            {
                new Restaurant { Id = 1, Name = "Scott's Pizza", Location="Maryland", Cuisine=CuisineType.Italian},
                new Restaurant { Id = 2, Name = "Bob's Burgers", Location="Palm Harbor", Cuisine=CuisineType.None},
                new Restaurant { Id = 3, Name = "Fairway Pizza", Location="TarponSpings", Cuisine=CuisineType.Indian}
            };
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return from r in restaurants
                   orderby r.Name
                   select r;
        }
    }
}