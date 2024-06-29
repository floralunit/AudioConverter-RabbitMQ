**Аудиоконвертер из wav в wma**
RabbitMQ: http://localhost:15672/, логин и пароль test
Проект ReceiveMessagesService подключается к RabbitMQ и создает там очередь audio-converter-endpoint, а также создает consumer, который ждет сообщение типа ConvertWavToWmaMessage, в котором указывается путь к wave файлу и путь, куда конвертировать в wma файл
Проект SendMessagesAPI тестовый проект, который подключается к RabbitMQ и позволяет отправлять post запрос типа ConvertWavToWmaMessage и сразу отправлять сообщение в очередь
