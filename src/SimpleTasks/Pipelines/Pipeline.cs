using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using SimpleTasks.Pipelines.Attributes;

namespace SimpleTasks.Pipelines
{
    public abstract class Pipeline<TNode, TInput, TContext>: VertexContainer<TNode, Pipeline<TNode, TInput, TContext>>
    where TNode : Vertex<TNode, Pipeline<TNode, TInput, TContext>>
    {
        private readonly IEnumerable<TNode> _nodes;

        protected Pipeline(IEnumerable<TNode> nodes,
            PipelineContainerConfiguration containerConfiguration) : base(containerConfiguration)
        {
            _nodes = nodes;
            
            foreach (var node in _nodes)
            {
                node.VertexContainer = this;
                Graph.InsertVertex(node.Name);

                var dependsOnList = (IEnumerable<DependsOnAttribute>) node
                    .GetType()
                    .GetCustomAttributes(typeof(DependsOnAttribute));

                foreach (var attribute in dependsOnList)
                {
                    node.DependsOn(attribute.Dependency.AssemblyQualifiedName);
                }
            }
        }
    }
}