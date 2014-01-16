using DefinityFirst.Core.Entities.Complex;
using DefinityFirst.Sample.Data;
using System;
using System.Linq;
using System.Data.Entity;
using DefinityFirst.Core.Entities.Simple;

namespace DefinityFirst.Core.Services
{
    public class PersonService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public PersonEntity GetPersonAndPhones(int personId)
        {
            using (AdventureWorksDb db = new AdventureWorksDb())
            {
                Person person =
                    db.Person
                      .Include(p => p.PersonPhone.Select(ph => ph.PhoneNumberType))
                      .Single(p => p.BusinessEntityID == personId);

                return new PersonEntity
                {
                    EmailPromotion = person.EmailPromotion,
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    MiddleName = person.MiddleName,
                    NameStyle = person.NameStyle,
                    PersonId = person.BusinessEntityID,
                    PersonType = person.PersonType,
                    Suffix = person.Suffix,
                    Title = person.Title,
                    Phones = (from p in person.PersonPhone
                              select new PhoneNumberEntity
                              {
                                  PhoneNumber = p.PhoneNumber,
                                  PhoneNumberType = p.PhoneNumberType.Name,
                                  PhoneNumberTypeId = p.PhoneNumberTypeID
                              }).ToList()
                };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public PersonEntity GetPersonAndPhonesUsingExplicitLoading(int personId)
        {
            using (AdventureWorksDb db = new AdventureWorksDb())
            {
                //1st query
                Person person =
                    db.Person
                      .Single(p => p.BusinessEntityID == personId);

                //2nd query (uses eager loading)
                db.Entry(person)
                  .Collection(p => p.PersonPhone)
                  .Query()
                  .Include(pnt => pnt.PhoneNumberType)
                  .Load();

                return new PersonEntity
                {
                    EmailPromotion = person.EmailPromotion,
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    MiddleName = person.MiddleName,
                    NameStyle = person.NameStyle,
                    PersonId = person.BusinessEntityID,
                    PersonType = person.PersonType,
                    Suffix = person.Suffix,
                    Title = person.Title,
                    Phones = (from p in person.PersonPhone
                              select new PhoneNumberEntity
                              {
                                  PhoneNumber = p.PhoneNumber,
                                  PhoneNumberType = p.PhoneNumberType.Name,
                                  PhoneNumberTypeId = p.PhoneNumberTypeID
                              }).ToList()
                };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int CreatePerson(PersonEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
