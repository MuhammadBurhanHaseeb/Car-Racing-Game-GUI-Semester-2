using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
namespace FrameWork.Core
{
    public class Game:IGame
    {
        private List<GameObject> gameObjects ;
        private List<GameObject> gameObjectsHealth;
        private List<ColisionClass> collisions;
        public event EventHandler onAddingGameObject;
        public event EventHandler onAddingGameObjectHealth;
        public event EventHandler playerDieEvent , onObjectRemove, onRemoveEnemy;
        private int height;
        public List<GameObject> GameObjects { get => gameObjects; set => gameObjects = value; }

        public Game(int height)
        {
            gameObjects = new List<GameObject>();
            collisions = new List<ColisionClass>();
            gameObjectsHealth = new List<GameObject>();
            this.height = height;
        }
        
        public void addGameObject(Image img, GameObjectName otherType, int top, int left, int width, int height , IMovemnt movement)
        {
          GameObject g = new   GameObject( img, otherType, top,  left, width , height,   movement);
            GameObjects.Add(g);
            onAddingGameObject?.Invoke(g.Pb , EventArgs.Empty);
        }
        public void addGameObject(int width , int height , int x , int y ,   GameObjectName otherType , IMovemnt movement)
        {
            GameObject g = new GameObject(width , height , x , y , otherType , movement);
            gameObjectsHealth.Add(g);
            onAddingGameObjectHealth?.Invoke(g.Health, EventArgs.Empty);
        }



        public void raisePlayerDieEvent(PictureBox playerGameObject)
        {
            playerDieEvent?.Invoke(playerGameObject ,EventArgs.Empty);
        }
      
        public void Update()
        {
            if (gameObjects.Count>0)
            {
                detectCollision();

            }
            foreach (GameObject a in gameObjectsHealth)
            {
                a.update();
            }

            foreach (GameObject s in GameObjects )
            {
                s.update();
            }
        }

        public void KeyPressed(Keys keyCode)
        {
            foreach(GameObject s in GameObjects)
            {
                if (s.Movement.GetType() == typeof(KeyBoard))// movement ki type mangwa lii grtType ki madad se or match kia ke keyBoard wali hai
                {
                    KeyBoard keyBoardHandel = (KeyBoard)s.Movement; 
                    keyBoardHandel.keyPressedByUser(keyCode);
                }
            }
        }

       public void detectCollision()
        {
            for(int x  = 0; x < gameObjects.Count; x++)
            {
                for (int y = 0; y < gameObjects.Count; y++)
                {
                    if (gameObjects[x].Pb.Bounds.IntersectsWith(gameObjects[y].Pb.Bounds))
                    {

                        foreach(ColisionClass c in collisions)
                        {

                            if ((gameObjects[x].OtherType == c.A) && (gameObjects[y].OtherType == c.B))
                            {
                                foreach (GameObject z in gameObjectsHealth)
                                {
                                   // if()
                                    //{

//                                    }
                                    if (z.OtherType == GameObjectName.playerHealth)
                                    {
                                        if (z.Health.Value - 10 <0)
                                        {
                                            z.Health.Value = 0;
                                        }
                                        else
                                        {
                                            z.Health.Value -= 10;
                                        }
                                        onRemoveEnemy?.Invoke(gameObjects[y].Pb , EventArgs.Empty);
                                        gameObjects.Remove(gameObjects[y]);
                                    }
                                }
                               
                               // c.ActionBehave.performAction(this,gameObjects[x] , gameObjects[y]);
                            }
                          /*  continue;

                            if ((gameObjects[x].OtherType == c.A) && (gameObjects[y].OtherType == c.B))
                            {
                                foreach (GameObject z in gameObjectsHealth)
                                {
                                    if (z.OtherType == GameObjectName.playerHealth)
                                    {
                                            z.Health.Value += 10;

                                    }
                                }
                                // c.ActionBehave.performAction(this,gameObjects[x] , gameObjects[y]);
                            }
                            continue;*/

                        }

                    }
                }

            }
        }
        public void addCollisions(ColisionClass c )
        {
            collisions.Add(c);
        }

        public void removeObjects()
        {
            for(int x = 0; x < gameObjects.Count; x++)
            {
                if (gameObjects[x].getObjectLocation().Y > this.height)
                {
                   
                    onObjectRemove?.Invoke(gameObjects[x].Pb, EventArgs.Empty);
                    gameObjects.Remove(gameObjects[x]);
                }
            }
        }
    }
}
