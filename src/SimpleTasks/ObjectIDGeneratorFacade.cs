using System.Runtime.Serialization;

namespace SimpleTasks
{
    public static class ObjectIDGeneratorFacade
    {
        private static ObjectIDGenerator ObjectIdGen { get; set; }

        static ObjectIDGeneratorFacade()
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
