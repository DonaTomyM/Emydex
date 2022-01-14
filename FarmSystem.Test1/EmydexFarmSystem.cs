using FarmSystem.Test2;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FarmSystem.Test1
{
    public abstract class FarmAnimals
    {
        public abstract void Enter(Object animal);
    }
    public class EmydexFarmSystem : FarmAnimals
    {

        private List<Object> animalList = new List<Object>();

        //TEST 1
        
        public override void Enter(Object animal)
        {
            //TODO Modify the code so that we can display the type of animal (cow, sheep etc) 
            //Hold all the animals so it is available for future activities
           
            animalList.Add(animal);
            Type objtype = animal.GetType();
            Console.Write(objtype.Name);
            Console.WriteLine(" has entered the Emydex farm");
        } 
     
        //TEST 2
        public void MakeNoise()
        {
            //Test 2 : Modify this method to make the animals talk
            foreach (Object animal in animalList)
            {
                dynamic animalType = animal;
                animalType.Talk();
            }
            
        }

        //TEST 3
        public void MilkAnimals()
        {
            foreach (Object animal in animalList)
            {
                dynamic dynAnimal = animal;

                if (animal is IMilkableAnimal)
                 {
                    dynAnimal.ProduceMilk();
                }
            }
        }

        //TEST 4
        public void ReleaseAllAnimals()
        {

            ReleaseAnimal RA = new ReleaseAnimal();
            RA.FarmEmpty += RA_FarmEmpty;
            RA.StartReleasing(animalList);
        }
        public static void RA_FarmEmpty(object sender, bool IsSucess)
        {
            Console.WriteLine("Emydex Farm is now empty");
        }
     }

    public class ReleaseAnimal
    {
        public event EventHandler<bool> FarmEmpty;

        public void StartReleasing(List<Object> animalList)
        {
            try
            {
                foreach (Object animal in animalList.ToList())
                {

                    Type objtype = animal.GetType();
                    Console.Write(objtype.Name);
                    Console.WriteLine(" has left the farm");
                    animalList.Remove(animal);
                }
                onFarmEmpty(true);
            }
            catch(Exception ex)
            {
                onFarmEmpty(false);
            }
        }
        protected virtual void onFarmEmpty(bool IsSuccess)
        {
            FarmEmpty?.Invoke(this, IsSuccess);
        }
    }
}