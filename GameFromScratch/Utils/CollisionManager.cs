using System.Collections.Generic;
using PlanetDefender.Entities;

namespace PlanetDefender.Utils
{
    public class CollisionManager
    {
        private readonly List<Entity> entities = new List<Entity>();

        public bool AddEntity(Entity entity)
        {
            if (entities.Contains(entity))
            {
                return false;
            }
            entities.Add(entity);
            return true;
        }

        public bool RemoveEntity(Entity entity)
        {
            return entities.Remove(entity);
        }

        public void Update()
        {
            for (int i = 0; i < entities.Count - 1; i++)
            {
                var entityA = entities[i];
                for (int j = i + 1; j < entities.Count; j++)
                {
                    var entityB = entities[j];
                    if (entityA.IgnoreCollision || entityB.IgnoreCollision) continue;
                    
                    if (entityA.Bounds.Intersects(entityB.Bounds))
                    {
                        entityA.OnCollision(entityB);
                        entityB.OnCollision(entityA);
                    }
                }
            }
        }
    }
}
