using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace Simulation
{
    class Car : ICloneable
    {
        public int Length = 60;
        public int Width = 30;
        public byte[][][] Bitmap;
        public Engine SimulationEngine;

        public PointF Centre { get => centre; set => CentreChanged(value); }
        public PointF RightFront;
        public PointF LeftFront;
        public PointF RightBack;
        public PointF LeftBack;

        public Brain Brain;
        public int BrainOutputs = 3;
        public int BrainHiddenLayers = 2;
        public int BrainNeuronsPerLayer = 8;

        public Eye[] Eyes;
        public bool Alive = true;
        public int Distance;
        public int Lifetime;

        public float Rotation = 0.0F;
        public float Speed = 0.0F;

        public float Weight = 10.0F;
        public float Acceleration = 0.0F;
        public float TurnSpeed = 0.0F;
        public float BreakStrength = 0.0F;
        public int EyeAmount;
        public int EyeMaxViewDistance = 100;
        public int Color;


        private PointF centre;
        private Func<float, float> activationFunc = x => (float)Math.Tanh(x);

        public Car(Point location, byte[][][] bitmap, Engine simEngine) // Random 
        {
            Bitmap = bitmap;
            SimulationEngine = simEngine;
            GenerateRndProperties();
            Centre = location;
        } 

        public Car(Point location, Car parent, byte[][][] bitmap, Engine simEngine) // Mutate 
        {
            Bitmap = bitmap;
            SimulationEngine = simEngine;
            MutateProperties(parent);
            Centre = location;
        } 

        public Car(Point location, string structure, byte[][][] bitmap, Engine simEngine) //Load 
        {
            Bitmap = bitmap;
            SimulationEngine = simEngine;
            LoadProperties(structure);
            Centre = location;
        }

        public Car(Point location, Stream structure, byte[][][] bitmap, Engine simEngine) //Load 
        {
            Bitmap = bitmap;
            SimulationEngine = simEngine;
            LoadProperties(structure);
            Centre = location;
        }

        public void GenerateRndProperties()
        {
            EyeAmount = 3;// RandomNumber.Between(2, 5);
            TurnSpeed = RandomNumber.Between(1, 50);
            Acceleration = RandomNumber.Between(1, 10);
            Color = RandomNumber.Between(0, 3);
            Eyes = new Eye[EyeAmount];

            /*for (int i = 0; i < Eyes.Length; i++)
            {
                Eyes[i] = new Eye(centre, RandomNumber.Between(Length, Length + 50), RandomNumber.Between(-90, 90), Bitmap);
            }*/
            Eyes[0] = new Eye(centre, EyeMaxViewDistance, Length, 10, -45, Bitmap, SimulationEngine);
            Eyes[1] = new Eye(centre, EyeMaxViewDistance, Length, 10, 0, Bitmap, SimulationEngine);
            Eyes[2] = new Eye(centre, EyeMaxViewDistance, Length, 10, 45, Bitmap, SimulationEngine);

            Brain = new Brain(EyeAmount, BrainOutputs, BrainHiddenLayers, BrainNeuronsPerLayer, -2.0F, 2.0F, true, activationFunc); //EyeAmount * 2 -> activates grid view
        }

        public void MutateProperties(Car parent)
        {
            EyeAmount = parent.EyeAmount;
            TurnSpeed = parent.TurnSpeed;
            Acceleration = parent.Acceleration;
            Color = RandomNumber.Between(0, 3);
            Eyes = new Eye[EyeAmount];

            for (int i = 0; i < Eyes.Length; i++)
            {
                Eyes[i] = new Eye(centre, parent.Eyes[i].MaxDistance, parent.Eyes[i].MinDistance, parent.Eyes[i].Location.Length, parent.Eyes[i].Rotation, Bitmap, SimulationEngine);
            }

            Brain = (Brain)parent.Brain.Clone();//new Brain(parent.Brain.BrainStructure, parent.Brain.Inputs.Length, BrainOutputs, BrainHiddenLayers, BrainNeuronsPerLayer);
            Brain = Training.MutateBrainStructure(Brain, 15, 3, 0.5F);
        }

        public void LoadProperties(string structure)
        {
            EyeAmount = 3;// RandomNumber.Between(2, 5);
            TurnSpeed = RandomNumber.Between(1, 50);
            Acceleration = RandomNumber.Between(1, 10);
            Color = RandomNumber.Between(0, 3);
            Eyes = new Eye[EyeAmount];

            Eyes[0] = new Eye(centre, EyeMaxViewDistance, Length, 10, -45, Bitmap, SimulationEngine);
            Eyes[1] = new Eye(centre, EyeMaxViewDistance, Length, 10, 0, Bitmap, SimulationEngine);
            Eyes[2] = new Eye(centre, EyeMaxViewDistance, Length, 10, 45, Bitmap, SimulationEngine);

            Brain = new Brain(structure, EyeAmount * 2, BrainOutputs, BrainHiddenLayers, BrainNeuronsPerLayer, activationFunc);
        }

        public void LoadProperties(Stream structure)
        {
            EyeAmount = 3;// RandomNumber.Between(2, 5);
            TurnSpeed = RandomNumber.Between(1, 50);
            Acceleration = RandomNumber.Between(1, 10);
            Color = RandomNumber.Between(0, 3);
            Eyes = new Eye[EyeAmount];

            Eyes[0] = new Eye(centre, EyeMaxViewDistance, Length, 10, -45, Bitmap, SimulationEngine);
            Eyes[1] = new Eye(centre, EyeMaxViewDistance, Length, 10, 0, Bitmap, SimulationEngine);
            Eyes[2] = new Eye(centre, EyeMaxViewDistance, Length, 10, 45, Bitmap, SimulationEngine);

            Brain = new Brain(structure, activationFunc);
        }

        public void Drive(int iteration)
        {
            if(Alive)
            {
                float speedX = (float)(Math.Cos(Rotation * 0.0174533) * Speed);
                float speedY = (float)(Math.Cos((90 - Rotation) * 0.0174533) * Speed);

                float[] input = new float[EyeAmount];
                float[] output;
                for (int i = 0; i < EyeAmount; i++)
                {
                    input[i] = Eyes[i].CheckGround();
                }
                /*for (int i = EyeAmount; i < EyeAmount * 2; i++)
                {
                    input[i] = Eyes[i - EyeAmount].CheckGrid();
                }*/

                Lifetime = iteration;
                output = Brain.Think(input, Neuron.Sigmoid);
                Rotation += (output[0] - output[1]) * 8;
                Speed = output[2] * 10;

                Centre = new PointF(Centre.X + speedX, Centre.Y + speedY);

                if (SimulationEngine.LoopSpeed >= 1.0F)
                {
                    Distance = GridUnit.GetByLocation(new Point(Convert.ToInt32(Centre.X), Convert.ToInt32(Centre.Y)), SimulationEngine.ParkourGrid, SimulationEngine.GridUnitSize).Value;
                }

            }
        }

        public void CentreChanged(PointF _centre)
        {
            centre = _centre;
            RightFront = new PointF(centre.X + Length / 2, centre.Y - Width / 2);
            LeftFront = new PointF(centre.X + Length / 2, centre.Y + Width / 2);
            RightBack = new PointF(centre.X - Length / 2, centre.Y - Width / 2);
            LeftBack = new PointF(centre.X - Length / 2, centre.Y + Width / 2);

            PointF CalcRotation(float x, float y, float rotation)
            {
                double xrot = Math.Cos(rotation * 0.0174533) * (x - centre.X) - Math.Sin(rotation * 0.0174533) * (y - centre.Y) + centre.X;
                double yrot = Math.Sin(rotation * 0.0174533) * (x - centre.X) + Math.Cos(rotation * 0.0174533) * (y - centre.Y) + centre.Y;

                return new PointF((float)xrot, (float)yrot);
            }

            RightFront = CalcRotation(RightFront.X, RightFront.Y, Rotation);
            LeftFront = CalcRotation(LeftFront.X, LeftFront.Y, Rotation);
            RightBack = CalcRotation(RightBack.X, RightBack.Y, Rotation);
            LeftBack = CalcRotation(LeftBack.X, LeftBack.Y, Rotation);

            foreach(Eye eye in Eyes)
            {
                eye.Centre = centre;
                for (int i = 0; i < eye.Location.Length; i++)
                {
                   eye.Location[i] = CalcRotation(centre.X + eye.MinDistance + eye.ReceptorDistance * i, centre.Y, Rotation + eye.Rotation);
                }

            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
