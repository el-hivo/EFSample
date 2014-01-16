using System.Collections.Generic;
using DefinityFirst.Core.Entities.Simple;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DefinityFirst.Core.Services;
using DefinityFirst.Core.Entities.Complex;

namespace DefinityFirst.Sample.Core.Tests
{
    [TestClass]
    public class PersonServiceTest
    {
        [TestMethod]
        public void GetPerson()
        {
            PersonService service = new PersonService();

            PersonEntity person = service.GetPersonAndPhones(10);

            Assert.IsNotNull(person);
            Assert.AreEqual(person.PersonId, 10);
            Assert.IsNotNull(person.Phones);
            Assert.AreEqual(person.Phones.Count, 1);
        }


        [TestMethod]
        public void GetPersonExplicitLoading()
        {
            PersonService service = new PersonService();

            PersonEntity person = service.GetPersonAndPhonesUsingExplicitLoading(10);

            Assert.IsNotNull(person);
            Assert.AreEqual(person.PersonId, 10);
            Assert.IsNotNull(person.Phones);
            Assert.AreEqual(person.Phones.Count, 1);
        }

        [TestMethod]
        public void CreatePersonTest()
        {
            PersonService service = new PersonService();

            PersonEntity person = new PersonEntity
            {
                EmailPromotion = 0,
                NameStyle = false,
                PersonType = "EM",
                Suffix = null,
                Title = "Mr.",
                FirstName = "Ivan",
                LastName = "Hernandez",
                Phones = new List<PhoneNumberEntity>
                {
                    new PhoneNumberEntity
                    {
                        PhoneNumber = "811-070-5641",
                        PhoneNumberTypeId = 1
                    },
                    new PhoneNumberEntity
                    {
                        PhoneNumber = "818-306-9051",
                        PhoneNumberTypeId = 2
                    }
                }
            };

            int personId = service.CreatePerson(person);

            Assert.IsTrue(personId >= 0);
        }
    }
}
