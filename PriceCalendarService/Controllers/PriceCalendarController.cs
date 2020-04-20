using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PriceCalendarService.Models;

namespace PriceCalendarService.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class PriceCalendarController : ControllerBase
    {
        public PriceCalendarController() 
        {

        }


        [HttpPost]
        public async Task<IActionResult> getInInterval([FromBody] GetItemPriceAndCurrencyCommand command ) 
        {
            var item = new ItemPriceAndCurrencyResponse();
            item.Currency = "DA";
            item.Groups = new System.Collections.Generic.List<Group>{
                new Group{
                    Description =  "Hytter",
                    Id = 1,
                    Items = new System.Collections.Generic.List<Item>{
                        new Item{
                            Name = "Hytte 1 ",
                            Id = Guid.NewGuid(),
                            Price = 200,
                            ItemDays = new System.Collections.Generic.List<ItemDay>{
                                new ItemDay{
                                    Date = DateTime.Now,
                                    Id = 1,
                                    price = 210,
                                    PricePackage = "Package 1",
                                    Priority = "High",
                                    CustomerType = new CustomerType{
                                        Description = "type1",
                                        id = Guid.NewGuid()
                                    }
                                },
                                new ItemDay{
                                    Date = DateTime.Now.AddDays(1),
                                    Id = 2,
                                    price = 230,
                                    PricePackage = "Package 2",
                                    Priority = "High",
                                    CustomerType = new CustomerType{
                                        Description = "type1",
                                        id = Guid.NewGuid()
                                    }
                                },
                                new ItemDay{
                                    Date = DateTime.Now.AddDays(2),
                                    Id = 3,
                                    price = 220,
                                    PricePackage = "Package 3",
                                    Priority = "High",
                                    CustomerType = new CustomerType{
                                        Description = "type1",
                                        id = Guid.NewGuid()
                                    }
                                }
                            }
                        },
                        new Item{
                            Name = "Hytte 2 ",
                            Id = Guid.NewGuid(),
                            Price = 300,
                            ItemDays = new System.Collections.Generic.List<ItemDay>{
                                new ItemDay{
                                    Date = DateTime.Now,
                                    Id = 4,
                                    price = 310,
                                    PricePackage = "Package 1",
                                    Priority = "High",
                                    CustomerType = new CustomerType{
                                        Description = "type1",
                                        id = Guid.NewGuid()
                                    }
                                },
                                new ItemDay{
                                    Date = DateTime.Now.AddDays(1),
                                    Id = 5,
                                    price = 330,
                                    PricePackage = "Package 2",
                                    Priority = "High",
                                    CustomerType = new CustomerType{
                                        Description = "type1",
                                        id = Guid.NewGuid()
                                    }
                                },
                                new ItemDay{
                                    Date = DateTime.Now.AddDays(2),
                                    Id = 6,
                                    price = 320,
                                    PricePackage = "Package 3",
                                    Priority = "High",
                                    CustomerType = new CustomerType{
                                        Description = "type1",
                                        id = Guid.NewGuid()
                                    }
                                }
                            }
                        },
                        new Item{
                            Name = "Hytte 3 ",
                            Id = Guid.NewGuid(),
                            Price = 100,
                            ItemDays = new System.Collections.Generic.List<ItemDay>{
                                new ItemDay{
                                    Date = DateTime.Now,
                                    Id = 7,
                                    price = 110,
                                    PricePackage = "Package 1",
                                    Priority = "High",
                                    CustomerType = new CustomerType{
                                        Description = "type1",
                                        id = Guid.NewGuid()
                                    }
                                },
                                new ItemDay{
                                    Date = DateTime.Now.AddDays(1),
                                    Id = 8,
                                    price = 130,
                                    PricePackage = "Package 2",
                                    Priority = "High",
                                    CustomerType = new CustomerType{
                                        Description = "type1",
                                        id = Guid.NewGuid()
                                    }
                                },
                                new ItemDay{
                                    Date = DateTime.Now.AddDays(2),
                                    Id = 9,
                                    price = 120,
                                    PricePackage = "Package 3",
                                    Priority = "High",
                                    CustomerType = new CustomerType{
                                        Description = "type1",
                                        id = Guid.NewGuid()
                                    }
                                }
                            }
                        }

                    }

                },
                 new Group{
                    Description =  "Varer",
                    Id = 2,
                    Items = new System.Collections.Generic.List<Item>{
                        new Item{
                            Name = "Juice",
                            Id = Guid.NewGuid(),
                            Price = 20,
                            ItemDays = new System.Collections.Generic.List<ItemDay>{
                                new ItemDay{
                                    Date = DateTime.Now,
                                    Id = 21,
                                    price = 21,
                                    PricePackage = "Package 2",
                                    Priority = "Low",
                                    CustomerType = new CustomerType{
                                        Description = "type1",
                                        id = Guid.NewGuid()
                                    }
                                },
                                new ItemDay{
                                    Date = DateTime.Now.AddDays(1),
                                    Id = 22,
                                    price = 22,
                                    PricePackage = "Package 2",
                                    Priority = "Low",
                                    CustomerType = new CustomerType{
                                        Description = "type1",
                                        id = Guid.NewGuid()
                                    }
                                },
                                new ItemDay{
                                    Date = DateTime.Now.AddDays(2),
                                    Id = 23,
                                    price = 19,
                                    PricePackage = "Package 3",
                                    Priority = "Low",
                                    CustomerType = new CustomerType{
                                        Description = "type1",
                                        id = Guid.NewGuid()
                                    }
                                }
                            }
                        },
                        new Item{
                            Name = "Smør ",
                            Id = Guid.NewGuid(),
                            Price = 18,
                            ItemDays = new System.Collections.Generic.List<ItemDay>{
                                new ItemDay{
                                    Date = DateTime.Now,
                                    Id = 24,
                                    price = 18,
                                    PricePackage = "Package 1",
                                    Priority = "Low",
                                    CustomerType = new CustomerType{
                                        Description = "type1",
                                        id = Guid.NewGuid()
                                    }
                                },
                                new ItemDay{
                                    Date = DateTime.Now.AddDays(1),
                                    Id = 25,
                                    price = 18,
                                    PricePackage = "Package 2",
                                    Priority = "Low",
                                    CustomerType = new CustomerType{
                                        Description = "type1",
                                        id = Guid.NewGuid()
                                    }
                                },
                                new ItemDay{
                                    Date = DateTime.Now.AddDays(2),
                                    Id = 26,
                                    price = 18,
                                    PricePackage = "Package 3",
                                    Priority = "Low",
                                    CustomerType = new CustomerType{
                                        Description = "type1",
                                        id = Guid.NewGuid()
                                    }
                                }
                            }
                        },
                        new Item{
                            Name = "Æg",
                            Id = Guid.NewGuid(),
                            Price = 25,
                            ItemDays = new System.Collections.Generic.List<ItemDay>{
                                new ItemDay{
                                    Date = DateTime.Now,
                                    Id = 27,
                                    price = 110,
                                    PricePackage = "Package 1",
                                    Priority = "Low",
                                    CustomerType = new CustomerType{
                                        Description = "type1",
                                        id = Guid.NewGuid()
                                    }
                                },
                                new ItemDay{
                                    Date = DateTime.Now.AddDays(1),
                                    Id = 28,
                                    price = 29,
                                    PricePackage = "Package 2",
                                    Priority = "Low",
                                    CustomerType = new CustomerType{
                                        Description = "type1",
                                        id = Guid.NewGuid()
                                    }
                                },
                                new ItemDay{
                                    Date = DateTime.Now.AddDays(2),
                                    Id = 29,
                                    price = 26,
                                    PricePackage = "Package 3",
                                    Priority = "Low",
                                    CustomerType = new CustomerType{
                                        Description = "type1",
                                        id = Guid.NewGuid()
                                    }
                                }
                            }
                        }

                    }

                }
            };
            return Ok(item);
        }
    
    }
}