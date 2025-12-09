using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TourSite.Core.Entities;
using TourSite.Repository.Data.Contexts;

namespace TourSite.Repository.Data
{
    public static class TourDbContextSeed
    {
        public static async Task SeedAsync(TourDbContext context)
        {


            #region Seeding

            if (context.TourIncludeds.Count() == 0)
            {
                // 1- Read data from file 
                var strings = File.ReadAllText(@"..\TourSite.Repository\Data\DataSeed\TourIncluded.json");

                if (string.IsNullOrEmpty(strings))
                {
                    throw new ArgumentException("The TourImgs.json file is empty or not found.");
                }
                // 2- Deserialize the JSON data into a list of ProductBrand objects

                var tour = JsonSerializer.Deserialize<List<TourIncluded>>(strings);

                if (tour is not null && tour.Count() > 0)
                {
                    await context.TourIncludeds.AddRangeAsync(tour);

                    // 3- Save changes to the database

                    await context.SaveChangesAsync();
                }

            }

            if (context.TourNotIncludeds.Count() == 0)
            {
                // 1- Read data from file 
                var strings = File.ReadAllText(@"..\TourSite.Repository\Data\DataSeed\TourNotIncluded.json");

                if (string.IsNullOrEmpty(strings))
                {
                    throw new ArgumentException("The TourImgs.json file is empty or not found.");
                }
                // 2- Deserialize the JSON data into a list of ProductBrand objects

                var tour = JsonSerializer.Deserialize<List<TourNotIncluded>>(strings);

                if (tour is not null && tour.Count() > 0)
                {
                    await context.TourNotIncludeds.AddRangeAsync(tour);

                    // 3- Save changes to the database

                    await context.SaveChangesAsync();
                }

            }

            if (context.TourHighLight.Count() == 0)
            {
                // 1- Read data from file 
                var strings = File.ReadAllText(@"..\TourSite.Repository\Data\DataSeed\TourHighlight.json");

                if (string.IsNullOrEmpty(strings))
                {
                    throw new ArgumentException("The TourImgs.json file is empty or not found.");
                }
                // 2- Deserialize the JSON data into a list of ProductBrand objects

                var tour = JsonSerializer.Deserialize<List<TourHighlight>>(strings);

                if (tour is not null && tour.Count() > 0)
                {
                    await context.TourHighLight.AddRangeAsync(tour);

                    // 3- Save changes to the database

                    await context.SaveChangesAsync();
                }

            }


            if (context.TransferIncludeds.Count() == 0)
            {
                // 1- Read data from file 
                var strings = File.ReadAllText(@"..\TourSite.Repository\Data\DataSeed\TransferIncluded.json");

                if (string.IsNullOrEmpty(strings))
                {
                    throw new ArgumentException("The TourImgs.json file is empty or not found.");
                }
                // 2- Deserialize the JSON data into a list of ProductBrand objects

                var transfer = JsonSerializer.Deserialize<List<TransferIncluded>>(strings);

                if (transfer is not null && transfer.Count() > 0)
                {
                    await context.TransferIncludeds.AddRangeAsync(transfer);

                    // 3- Save changes to the database

                    await context.SaveChangesAsync();
                }

            }

            if (context.TransferNotIncludeds.Count() == 0)
            {
                // 1- Read data from file 
                var strings = File.ReadAllText(@"..\TourSite.Repository\Data\DataSeed\TransferNotIncluded.json");

                if (string.IsNullOrEmpty(strings))
                {
                    throw new ArgumentException("The TourImgs.json file is empty or not found.");
                }
                // 2- Deserialize the JSON data into a list of ProductBrand objects

                var transfer = JsonSerializer.Deserialize<List<TransferNotIncluded>>(strings);

                if (transfer is not null && transfer.Count() > 0)
                {
                    await context.TransferNotIncludeds.AddRangeAsync(transfer);

                    // 3- Save changes to the database

                    await context.SaveChangesAsync();
                }

            }

            if (context.TransferHighLights.Count() == 0)
            {
                // 1- Read data from file 
                var strings = File.ReadAllText(@"..\TourSite.Repository\Data\DataSeed\TransferHighlight.json");
                if (string.IsNullOrEmpty(strings))
                {
                    throw new ArgumentException("The TourImgs.json file is empty or not found.");
                }
                // 2- Deserialize the JSON data into a list of ProductBrand objects
                var transfer = JsonSerializer.Deserialize<List<TransferHighlight>>(strings);
                if (transfer is not null && transfer.Count() > 0)
                {
                    await context.TransferHighLights.AddRangeAsync(transfer);
                    // 3- Save changes to the database
                    await context.SaveChangesAsync();
                }
            }

            if (context.PricesList.Count() == 0)
            {
                // 1- Read data from file 
                var strings = File.ReadAllText(@"..\TourSite.Repository\Data\DataSeed\transfersPrices.json");

                if (string.IsNullOrEmpty(strings))
                {
                    throw new ArgumentException("The TourImgs.json file is empty or not found.");
                }
                // 2- Deserialize the JSON data into a list of ProductBrand objects

                var priece = JsonSerializer.Deserialize<List<TrasnferPrices>>(strings);

                if (priece is not null && priece.Count() > 0)
                {
                    await context.PricesList.AddRangeAsync(priece);

                    // 3- Save changes to the database

                    await context.SaveChangesAsync();
                }

            }

            if (context.Transfers.Count() == 0)
            {
                // 1- Read data from file 
                var strings = File.ReadAllText(@"..\TourSite.Repository\Data\DataSeed\transfers.json");

                if (string.IsNullOrEmpty(strings))
                {
                    throw new ArgumentException("The Transfers.json file is empty or not found.");
                }
                // 2- Deserialize the JSON data into a list of ProductBrand objects

                var transfers = JsonSerializer.Deserialize<List<Transfer>>(strings);

                if (transfers is not null && transfers.Count() > 0)
                {
                    await context.Transfers.AddRangeAsync(transfers);

                    // 3- Save changes to the database

                    await context.SaveChangesAsync();
                }

            }

            if (context.Emails.Count() == 0)
            {
                // 1- Read data from file 
                var strings = File.ReadAllText(@"..\TourSite.Repository\Data\DataSeed\emails.json");

                if (string.IsNullOrEmpty(strings))
                {
                    throw new ArgumentException("The Emails.json file is empty or not found.");
                }
                // 2- Deserialize the JSON data into a list of ProductBrand objects

                var emails = JsonSerializer.Deserialize<List<Email>>(strings);

                if (emails is not null && emails.Count() > 0)
                {
                    await context.Emails.AddRangeAsync(emails);

                    // 3- Save changes to the database

                    await context.SaveChangesAsync();
                }

            }


            if (context.Users.Count() == 0)
            {
                // 1- Read data from file 
                var strings = File.ReadAllText(@"..\TourSite.Repository\Data\DataSeed\users.json");

                if (string.IsNullOrEmpty(strings))
                {
                    throw new ArgumentException("The Users.json file is empty or not found.");
                }
                // 2- Deserialize the JSON data into a list of ProductBrand objects

                var users = JsonSerializer.Deserialize<List<User>>(strings);

                if (users is not null && users.Count() > 0)
                {
                    await context.Users.AddRangeAsync(users);

                    // 3- Save changes to the database

                    await context.SaveChangesAsync();
                }

            }

            if (context.CategoryTours.Count() == 0)
            {
                // 1- Read data from file 
                var strings = File.ReadAllText(@"..\TourSite.Repository\Data\DataSeed\categorytours.json");

                if (string.IsNullOrEmpty(strings))
                {
                    throw new ArgumentException("The CategoryTours.json file is empty or not found.");
                }
                // 2- Deserialize the JSON data into a list of ProductBrand objects

                var categoryTours = JsonSerializer.Deserialize<List<CategoryTour>>(strings);

                if (categoryTours is not null && categoryTours.Count() > 0)
                {
                    await context.CategoryTours.AddRangeAsync(categoryTours);

                    // 3- Save changes to the database

                    await context.SaveChangesAsync();
                }

            }

            if (context.Destinations.Count() == 0)
            {
                // 1- Read data from file 
                var strings = File.ReadAllText(@"..\TourSite.Repository\Data\DataSeed\destinations.json");

                if (string.IsNullOrEmpty(strings))
                {
                    throw new ArgumentException("The Destinations.json file is empty or not found.");
                }
                // 2- Deserialize the JSON data into a list of ProductBrand objects

                var destinations = JsonSerializer.Deserialize<List<Destination>>(strings);

                if (destinations is not null && destinations.Count() > 0)
                {
                    await context.Destinations.AddRangeAsync(destinations);

                    // 3- Save changes to the database

                    await context.SaveChangesAsync();
                }

            }


            if (context.Tours.Count() == 0)
            {
                // 1- Read data from file 
                var strings = File.ReadAllText(@"..\TourSite.Repository\Data\DataSeed\tours.json");

                if (string.IsNullOrEmpty(strings))
                {
                    throw new ArgumentException("The Tours.json file is empty or not found.");
                }
                // 2- Deserialize the JSON data into a list of ProductBrand objects

                var tours = JsonSerializer.Deserialize<List<Tour>>(strings);

                if (tours is not null && tours.Count() > 0)
                {
                    await context.Tours.AddRangeAsync(tours);

                    // 3- Save changes to the database

                    await context.SaveChangesAsync();
                }

            }

            if (context.TourImgs.Count() == 0)
            {
                // 1- Read data from file 
                var strings = File.ReadAllText(@"..\TourSite.Repository\Data\DataSeed\tourImgs.json");

                if (string.IsNullOrEmpty(strings))
                {
                    throw new ArgumentException("The TourImgs.json file is empty or not found.");
                }
                // 2- Deserialize the JSON data into a list of ProductBrand objects

                var tourImgs = JsonSerializer.Deserialize<List<TourImg>>(strings);

                if (tourImgs is not null && tourImgs.Count() > 0)
                {
                    await context.TourImgs.AddRangeAsync(tourImgs);

                    // 3- Save changes to the database

                    await context.SaveChangesAsync();
                }

            }


         









            //////////////////////
            ///
          
            //////////--//////////
            ///
            ///
          

            #endregion


        }
    }
}
