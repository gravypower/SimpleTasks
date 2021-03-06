﻿using System.Linq;
using SimpleTasks.GraphTheory.Algorithms;
using SimpleTasks.GraphTheory.Graphs;

namespace SimpleTasks
{
    public abstract class VertexContainer<TVertex, TVertexContainer> 
        where TVertex : Vertex<TVertex, TVertexContainer>
        where TVertexContainer : VertexContainer<TVertex, TVertexContainer>
    {
        protected readonly VertexContainerConfiguration ContainerConfiguration;
        protected readonly DirectedAcyclicGraph<string> Graph  = new DirectedAcyclicGraph<string>();
        
        protected VertexContainer(VertexContainerConfiguration containerConfiguration)
        {
            ContainerConfiguration = containerConfiguration;
        }
        
        public void RegisterDependency(string source, string target)
        {
            Graph.InsertEdge(source, target);
        }
        
        public bool DoesDependencyExist(string source, string target)
        {
            return Graph.Edges.Any(e => e.Source == source && e.Target == target);
        }
        
        public bool DoesContainVertex(string vertexName)
        {
            return Graph.Vertices.Contains(vertexName);
        }

        public void Run()
        {
            foreach (var sortedVertexName in Graph.TopologicalSort())
            {
                DoRun(sortedVertexName);
            }
        }
        
        public void RegisterEmptyDependency(string taskName)
        {
            Graph.InsertVertex(taskName);
        }

        protected abstract void DoRun(string vertexName);
    }
}