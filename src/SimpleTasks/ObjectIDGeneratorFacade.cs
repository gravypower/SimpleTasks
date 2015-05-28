using System.Runtime.Serialization;

namespace SimpleTasks
{
    public static class ObjectIdGeneratorFacade
    {
        private static ObjectIDGenerator ObjectIdGen { get; set; }

        static ObjectIdGeneratorFacade()
        {
            ObjectIdGen = new ObjectIDGenerator();
        }

        public static string GetId(object obj)
        {
            bool firstTime;
            return ObjectIdGen.GetId(obj, out firstTime) + "_ObjectIDGeneratorFacade";
        }
    }
}
