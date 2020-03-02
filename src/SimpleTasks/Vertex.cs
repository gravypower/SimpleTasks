using System.Collections.Generic;
using System.Linq;
using SimpleTasks.Annotations;
using SimpleTasks.Exceptions;

namespace SimpleTasks
{
    public abstract class Vertex<TVertex, TVertexContainer> 
        where TVertex : Vertex<TVertex, TVertexContainer>
        where TVertexContainer : VertexContainer<TVertex, TVertexContainer>
    {
        public TVertexContainer VertexContainer { get; set; }

        protected Vertex(
            TVertexContainer vertexContainer,
            string name)
        {
            Name = name;
            VertexContainer = vertexContainer;
        }

        protected Vertex()
        {
        }

        public string Name { get; protected set; }
        
        public bool Invoked { get; protected set; }
        
        public TVertex DependsOn(params object[] otherTasks)
        {
            foreach (var taskId in otherTasks.Select(ObjectIdGeneratorFacade.GetId))
            {
                if (!VertexContainer.DoesContainVertex(taskId))
                    VertexContainer.RegisterEmptyDependency(taskId);

                DependsOn(taskId);
            }

            return this as TVertex;
        }
        
        public TVertex DependsOn(params string[] otherTasks)
        {
            GuardOtherTasks(otherTasks);

            foreach (var otherTask in otherTasks)
            {
                if (!VertexContainer.DoesContainVertex(otherTask))
                    VertexContainer.RegisterEmptyDependency(otherTask);

                VertexContainer.RegisterDependency(otherTask, Name);
            }
            return this as TVertex;
        }
        
        [AssertionMethod]
        private void GuardOtherTasks([NotNull] IReadOnlyList<string> otherTasks)
        {
            if (otherTasks == null)
                throw new ArgumentNullOrEmptyException("otherTasks");

            for (var i = 0; i < otherTasks.Count; i++)
            {
                var otherTask = otherTasks[i];
                GuardOtherTask(otherTask, i);
            }
        }

        [AssertionMethod]
        private void GuardOtherTask(string otherTask, int i)
        {
            if (string.IsNullOrEmpty(otherTask))
                throw new ArgumentNullOrEmptyException("otherTasks at index " + i);

            if (VertexContainer.DoesDependencyExist(otherTask, Name))
                throw new DependencyExistException();
        }
        
        
        
    }
}