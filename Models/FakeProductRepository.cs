using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace animated_spoon.Models
{
    public class FakeProductRepository : IProductRepository
    {
        public IQueryable<Product> Products => new List<Product> 
        {
            new Product { Name = "Subaru Impreza", Description = "The Subaru Impreza is a compact car that has been manufactured by Subaru since 1992. It was introduced as a replacement for the Leone, with the predecessor's EA series engines replaced by the new EJ series. It is now in its fifth generation.", Price = 31360 },
            new Product { Name = "Volvo S60", Description = "The Volvo S60 is a compact executive car manufactured and marketed by Volvo since 2000 and is now in its third generation. The first generation was launched in autumn of 2000 in order to replace the S70 and was based on the P2 platform. It had a similar designed estate version called Volvo V70 and a sports version called S60 R. Styling cues were taken from the ECC concept car and the S80.", Price = 50000 },
            new Product { Name = "Peugeot 508", Description = "The Peugeot 508 is a large family car launched in October 2010 by French automaker Peugeot, and followed by the 508 SW, an estate version, in March 2011. It replaces the Peugeot 407, as well as the larger Peugeot 607, for which no direct replacement was scheduled. It shares its platform and most engine options with the second generation Citroën C5: the two cars are produced alongside one another at the company's Rennes Plant, and in Wuhan, China, for sales inside China.", Price = 38000}
        }.AsQueryable<Product>();
    }
}
