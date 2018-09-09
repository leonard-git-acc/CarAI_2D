using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation
{
    static class Training
    {
        public static Brain MutateBrainStructure(Brain brain, int weakMutationPercentage, int heavyMutationPercentage, float mutationRange)
        {
            int rndNum = RandomNumber.Between(0, 100);

            if (rndNum > 90)
            {
                for (int iteration = 0; iteration < brain.AllLayers.Length; iteration++)
                {
                    for (int iter = 0; iter < brain.AllLayers[iteration].Length; iter++)
                    {
                        Neuron targetNeuron = brain.AllLayers[iteration][iter];
                        rndNum = RandomNumber.Between(0, 100);

                        if (rndNum > 100 - heavyMutationPercentage && targetNeuron.Type != Neuron.NeuronType.InputNeuron)
                        {
                            for (int i = 0; i < targetNeuron.Weight.Length; i++)
                            {
                                rndNum = RandomNumber.Between(0, 100);

                                if (rndNum > 100 - heavyMutationPercentage)
                                {
                                    int mutationRangeInt = Convert.ToInt32(mutationRange * 100);
                                    float mutation = (float)RandomNumber.Between(mutationRangeInt * -1, mutationRangeInt) / 100;
                                    targetNeuron.Weight[i] += mutation;
                                }
                            }
                        }
                    }
                }
            }

            else if (rndNum > 50)
            {
                for (int iteration = 0; iteration < brain.AllLayers.Length; iteration++)
                {
                    for (int iter = 0; iter < brain.AllLayers[iteration].Length; iter++)
                    {
                        Neuron targetNeuron = brain.AllLayers[iteration][iter];
                        rndNum = RandomNumber.Between(0, 100);

                        if (rndNum > 100 - weakMutationPercentage && targetNeuron.Type != Neuron.NeuronType.InputNeuron)
                        {
                            for (int i = 0; i < targetNeuron.Weight.Length; i++)
                            {
                                rndNum = RandomNumber.Between(0, 100);

                                if (rndNum > 100 - weakMutationPercentage)
                                {
                                    int mutationRangeInt = Convert.ToInt32(mutationRange / 4 * 100);
                                    float mutation = (float)RandomNumber.Between(mutationRangeInt * -1, mutationRangeInt) / 100;
                                    targetNeuron.Weight[i] += mutation;
                                }
                            }
                        }
                    }
                }
            }

            return brain;
        }
    }
}
