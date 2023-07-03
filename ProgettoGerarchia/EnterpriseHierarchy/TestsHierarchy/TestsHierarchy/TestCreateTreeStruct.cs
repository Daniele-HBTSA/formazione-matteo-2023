using EnterpriseHierarchy.Context;
using EnterpriseHierarchy.Models;
using EnterpriseHierarchy.Services.Implementations;
using FluentAssertions;
using Xunit;

namespace TestsHierarchy
{
    public class TestCreateTreeStruct
    {
        [Fact]
        public void Should_Create_TreeStructure_Form_DB_Data()
        {
            List<ENTERPRISES> treeParam = new List<ENTERPRISES>();
            treeParam.Add(new ENTERPRISES()
            {
                ID_ENTERPRISE = 1,
                ENT_PARENT_ID = null,
                ENT_BALACE = 1000
            });
            treeParam.Add(new ENTERPRISES()
            {
                ID_ENTERPRISE = 2,
                ENT_PARENT_ID = 1,
                ENT_BALACE = 200
            });
            treeParam.Add(new ENTERPRISES()
            {
                ID_ENTERPRISE = 3,
                ENT_PARENT_ID = 1,
                ENT_BALACE = 300
            });

            List<EnterpriseTree> expRes = new List<EnterpriseTree>()
            {
                new EnterpriseTree()
                {
                    Id = 1,
                    Balance = 1000,
                    Children = new List<EnterpriseTree>
                    {
                        new EnterpriseTree()
                        {
                            Id = 2,
                            Balance = 200,
                            Children = new List<EnterpriseTree>()
                        },
                        new EnterpriseTree()
                        {
                            Id= 3,
                            Balance = 300,
                            Children = new List<EnterpriseTree>()
                        }
                    }
                }
            };

            List<EnterpriseTree> actual = new EnterpriseTreeService().CreateTree(treeParam);

            Assert.Equivalent(expRes, actual, strict: true);
        }

        //Metodo che effettua la somma di tutti i saldi di ogni azienda
        [Fact]
        public void Should_Return_The_Sum_Of_Every_Balance_In_A_Tree()
        {
            List<EnterpriseTree> treeParam = new List<EnterpriseTree>()
            {
                new EnterpriseTree()
                {
                    Id = 1,
                    Balance = 1000,
                    Children = new List<EnterpriseTree>
                    {
                        new EnterpriseTree()
                        {
                            Id = 2,
                            Balance = 200,
                            Children = new List<EnterpriseTree>()
                            {
                                new EnterpriseTree()
                                {
                                    Id = 4,
                                    Balance = 400,
                                    Children = new List<EnterpriseTree>()
                                }
                            }
                        },
                        new EnterpriseTree()
                        {
                            Id= 3,
                            Balance = 300,
                            Children = new List<EnterpriseTree>()
                            {
                                new EnterpriseTree()
                                {
                                    Id= 5,
                                    Balance = 100,
                                    Children = new List<EnterpriseTree>()
                                    {
                                        new EnterpriseTree()
                                        {
                                            Id= 6,
                                            Balance = 700,
                                            Children = new List<EnterpriseTree>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };
            int expRes = 2700;

            int actualRes = new EnterpriseTreeService().SumEveryEnterpriseTreeBalances(treeParam);

            Assert.Equal(expRes, actualRes);
        }

        //Metodo che effettua la somma di tutti i saldi di una specifica azienda
        [Fact]
        public void Should_Return_The_Sum_Of_Every_Balance_In_A_Specific_Tree()
        {
            List<EnterpriseTree> treeParam = new List<EnterpriseTree>()
            {
                new EnterpriseTree()
                {
                    Id = 1,
                    Balance = 1000,
                    Children = new List<EnterpriseTree>
                    {
                        new EnterpriseTree()
                        {
                            Id = 2,
                            Balance = 200,
                            Children = new List<EnterpriseTree>()
                            {
                                new EnterpriseTree()
                                {
                                    Id = 4,
                                    Balance = 400,
                                    Children = new List<EnterpriseTree>()
                                }
                            }
                        },
                        new EnterpriseTree()
                        {
                            Id= 3,
                            Balance = 300,
                            Children = new List<EnterpriseTree>()
                            {
                                new EnterpriseTree()
                                {
                                    Id= 5,
                                    Balance = 100,
                                    Children = new List<EnterpriseTree>()
                                    {
                                        new EnterpriseTree()
                                        {
                                            Id= 6,
                                            Balance = 700,
                                            Children = new List<EnterpriseTree>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                new EnterpriseTree()
                {
                    Id = 10,
                    Balance = 500,
                    Children = new List<EnterpriseTree>
                    {
                        new EnterpriseTree()
                        {
                            Id = 11,
                            Balance = 200,
                            Children = new List<EnterpriseTree>
                            {
                                new EnterpriseTree()
                                {
                                    Id= 12,
                                    Balance = 300,
                                    Children = new List<EnterpriseTree>
                                    {
                                        new EnterpriseTree()
                                        {
                                            Id= 13,
                                            Balance = 100,
                                            Children = new List<EnterpriseTree>()
                                        }
                                    }
                                }
                            }
                        },
                        new EnterpriseTree()
                        {
                            Id = 14,
                            Balance = 600,
                            Children = new List<EnterpriseTree>
                            {
                                new EnterpriseTree()
                                {
                                    Id= 15,
                                    Balance = 400,
                                    Children = new List<EnterpriseTree>()
                                }
                            }
                        }
                    }
                },
                new EnterpriseTree()
                {
                    Id = 100,
                    Balance = 3000,
                    Children = new List<EnterpriseTree>()
                }
            };

            int fatherID = 10;

            int expRes = 2100;

            int actualRes = new EnterpriseTreeService().SumSpecificEnterpriseTreeBalances(treeParam, fatherID);

            Assert.Equal(expRes, actualRes);
        }

        //Metodo che cambia lo stato Selected ad una specifica azienda
        [Fact]
        public void Should_Change_Selected_Value_Of_Specific_Enterprise_And_Return_The_Updated_Tree()
        {
            List<EnterpriseTree> treeParam = new List<EnterpriseTree>()
            {
                new EnterpriseTree()
                {
                    Id = 1,
                    Balance = 1000,
                    Children = new List<EnterpriseTree>
                    {
                        new EnterpriseTree()
                        {
                            Id = 2,
                            Balance = 200,
                            Children = new List<EnterpriseTree>()
                            {
                                new EnterpriseTree()
                                {
                                    Id = 4,
                                    Balance = 400,
                                    Children = new List<EnterpriseTree>()
                                }
                            }
                        },
                        new EnterpriseTree()
                        {
                            Id= 3,
                            Balance = 300,
                            Children = new List<EnterpriseTree>()
                            {
                                new EnterpriseTree()
                                {
                                    Id= 5,
                                    Balance = 100,
                                    Children = new List<EnterpriseTree>()
                                    {
                                        new EnterpriseTree()
                                        {
                                            Id= 6,
                                            Balance = 700,
                                            Children = new List<EnterpriseTree>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                new EnterpriseTree()
                {
                    Id = 10,
                    Balance = 500,
                    Children = new List<EnterpriseTree>
                    {
                        new EnterpriseTree()
                        {
                            Id = 11,
                            Balance = 200,
                            Children = new List<EnterpriseTree>
                            {
                                new EnterpriseTree()
                                {
                                    Id= 12,
                                    Balance = 300,
                                    Children = new List<EnterpriseTree>
                                    {
                                        new EnterpriseTree()
                                        {
                                            Id= 13,
                                            Balance = 100,
                                            Children = new List<EnterpriseTree>()
                                        }
                                    }
                                }
                            }
                        },
                        new EnterpriseTree()
                        {
                            Id = 14,
                            Balance = 600,
                            Children = new List<EnterpriseTree>
                            {
                                new EnterpriseTree()
                                {
                                    Id= 15,
                                    Balance = 400,
                                    Children = new List<EnterpriseTree>()
                                }
                            }
                        }
                    }
                },
                new EnterpriseTree()
                {
                    Id = 100,
                    Balance = 3000,
                    Children = new List<EnterpriseTree>()
                }
            };
            int selectedID = 6;

            List<EnterpriseTree> expRes = new List<EnterpriseTree>()
            {
                new EnterpriseTree()
                {
                    Id = 1,
                    Balance = 1000,
                    Children = new List<EnterpriseTree>
                    {
                        new EnterpriseTree()
                        {
                            Id = 2,
                            Balance = 200,
                            Children = new List<EnterpriseTree>()
                            {
                                new EnterpriseTree()
                                {
                                    Id = 4,
                                    Balance = 400,
                                    Children = new List<EnterpriseTree>()
                                }
                            }
                        },
                        new EnterpriseTree()
                        {
                            Id= 3,
                            Balance = 300,
                            Children = new List<EnterpriseTree>()
                            {
                                new EnterpriseTree()
                                {
                                    Id= 5,
                                    Balance = 100,
                                    Children = new List<EnterpriseTree>()
                                    {
                                        new EnterpriseTree()
                                        {
                                            Id= 6,
                                            Balance = 700,
                                            Selected = true,
                                            Children = new List<EnterpriseTree>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                new EnterpriseTree()
                {
                    Id = 10,
                    Balance = 500,
                    Children = new List<EnterpriseTree>
                    {
                        new EnterpriseTree()
                        {
                            Id = 11,
                            Balance = 200,
                            Children = new List<EnterpriseTree>
                            {
                                new EnterpriseTree()
                                {
                                    Id= 12,
                                    Balance = 300,
                                    Children = new List<EnterpriseTree>
                                    {
                                        new EnterpriseTree()
                                        {
                                            Id= 13,
                                            Balance = 100,
                                            Children = new List<EnterpriseTree>()
                                        }
                                    }
                                }
                            }
                        },
                        new EnterpriseTree()
                        {
                            Id = 14,
                            Balance = 600,
                            Children = new List<EnterpriseTree>
                            {
                                new EnterpriseTree()
                                {
                                    Id= 15,
                                    Balance = 400,
                                    Children = new List<EnterpriseTree>()
                                }
                            }
                        }
                    }
                },
                new EnterpriseTree()
                {
                    Id = 100,
                    Balance = 3000,
                    Children = new List<EnterpriseTree>()
                }
            };

            List<EnterpriseTree> actualRes = new EnterpriseTreeService().SelectSpecificEnterprise(treeParam, selectedID);

            expRes.Should().BeEquivalentTo(actualRes);
        }

        //Metodi che cambia lo stato Selected ad uno specifico tree
        [Fact]
        public void Should_Change_Selected_Value_Of_Every_Enterprise_In_A_Specific_Tree_And_Return_The_Updated_Tree()
        {
            List<EnterpriseTree> treeParam = new List<EnterpriseTree>()
            {
                new EnterpriseTree()
                {
                    Id = 1,
                    Balance = 1000,
                    Children = new List<EnterpriseTree>
                    {
                        new EnterpriseTree()
                        {
                            Id = 2,
                            Balance = 200,
                            Children = new List<EnterpriseTree>()
                            {
                                new EnterpriseTree()
                                {
                                    Id = 4,
                                    Balance = 400,
                                    Children = new List<EnterpriseTree>()
                                }
                            }
                        },
                        new EnterpriseTree()
                        {
                            Id= 3,
                            Balance = 300,
                            Children = new List<EnterpriseTree>()
                            {
                                new EnterpriseTree()
                                {
                                    Id= 5,
                                    Balance = 100,
                                    Children = new List<EnterpriseTree>()
                                    {
                                        new EnterpriseTree()
                                        {
                                            Id= 6,
                                            Balance = 700,
                                            Children = new List<EnterpriseTree>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                new EnterpriseTree()
                {
                    Id = 10,
                    Balance = 500,
                    Children = new List<EnterpriseTree>
                    {
                        new EnterpriseTree()
                        {
                            Id = 11,
                            Balance = 200,
                            Children = new List<EnterpriseTree>
                            {
                                new EnterpriseTree()
                                {
                                    Id= 12,
                                    Balance = 300,
                                    Children = new List<EnterpriseTree>
                                    {
                                        new EnterpriseTree()
                                        {
                                            Id= 13,
                                            Balance = 100,
                                            Children = new List<EnterpriseTree>()
                                        }
                                    }
                                }
                            }
                        },
                        new EnterpriseTree()
                        {
                            Id = 14,
                            Balance = 600,
                            Children = new List<EnterpriseTree>
                            {
                                new EnterpriseTree()
                                {
                                    Id= 15,
                                    Balance = 400,
                                    Children = new List<EnterpriseTree>()
                                }
                            }
                        }
                    }
                },
                new EnterpriseTree()
                {
                    Id = 100,
                    Balance = 3000,
                    Children = new List<EnterpriseTree>()
                }
            };
            int fatherID = 1;

            List<EnterpriseTree> expRes = new List<EnterpriseTree>()
            {
                new EnterpriseTree()
                {
                    Id = 1,
                    Balance = 1000,
                    Selected = true,
                    Children = new List<EnterpriseTree>
                    {
                        new EnterpriseTree()
                        {
                            Id = 2,
                            Balance = 200,
                            Selected = true,
                            Children = new List<EnterpriseTree>()
                            {
                                new EnterpriseTree()
                                {
                                    Id = 4,
                                    Balance = 400,
                                    Selected = true,
                                    Children = new List<EnterpriseTree>()
                                }
                            }
                        },
                        new EnterpriseTree()
                        {
                            Id= 3,
                            Balance = 300,
                            Selected = true,
                            Children = new List<EnterpriseTree>()
                            {
                                new EnterpriseTree()
                                {
                                    Id= 5,
                                    Balance = 100,
                                    Selected = true,
                                    Children = new List<EnterpriseTree>()
                                    {
                                        new EnterpriseTree()
                                        {
                                            Id= 6,
                                            Balance = 700,
                                            Selected = true,
                                            Children = new List<EnterpriseTree>()
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                new EnterpriseTree()
                {
                    Id = 10,
                    Balance = 500,
                    Children = new List<EnterpriseTree>
                    {
                        new EnterpriseTree()
                        {
                            Id = 11,
                            Balance = 200,
                            Children = new List<EnterpriseTree>
                            {
                                new EnterpriseTree()
                                {
                                    Id= 12,
                                    Balance = 300,
                                    Children = new List<EnterpriseTree>
                                    {
                                        new EnterpriseTree()
                                        {
                                            Id= 13,
                                            Balance = 100,
                                            Children = new List<EnterpriseTree>()
                                        }
                                    }
                                }
                            }
                        },
                        new EnterpriseTree()
                        {
                            Id = 14,
                            Balance = 600,
                            Children = new List<EnterpriseTree>
                            {
                                new EnterpriseTree()
                                {
                                    Id= 15,
                                    Balance = 400,
                                    Children = new List<EnterpriseTree>()
                                }
                            }
                        }
                    }
                },
                new EnterpriseTree()
                {
                    Id = 100,
                    Balance = 3000,
                    Children = new List<EnterpriseTree>()
                }
            };

            List<EnterpriseTree> actualRes = new EnterpriseTreeService().SelectSpecificEnterpriseTree(treeParam, fatherID);

            expRes.Should().BeEquivalentTo(actualRes);
        }
    } 
}
