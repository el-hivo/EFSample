using System;
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
            Assert.AreEqual(person.Phones.Count, 2);
        }
    }
}
