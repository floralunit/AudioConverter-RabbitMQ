using System;
using System.Threading.Tasks;
using MassTransit;
using SendMessagesApi;
using System.IO;
using NAudio.Wave;

namespace ReceiveMessagesService
{
    public class AudioConsumer : IConsumer<ConvertWavToWmaMessage>
    {
        public Task Consume(ConsumeContext<ConvertWavToWmaMessage> context)
        {
            var value = $"Received: audio source ({context.Message.SourceFilePath}), destination ({context.Message.DestinationFilePath})";
            Console.WriteLine(value);
            ConvertWavToWma(context.Message.SourceFilePath, context.Message.DestinationFilePath);
            return Task.CompletedTask;
        }
        public void ConvertWavToWma(string inputWavFile, string outputWmaFile)
        {
            try
            {
                using (var reader = new WaveFileReader(inputWavFile))
                {
                    MediaFoundationEncoder.EncodeToWma(reader, outputWmaFile);
                }
                Console.WriteLine("WAV-файл успешно преобразован в wma.");
                File.Delete(inputWavFile);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Файл не найден");
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

}