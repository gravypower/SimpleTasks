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
            return ObjectIdGen.GetId(obj, out _) + "_ObjectIDGeneratorFacade";
        }
    }
}
