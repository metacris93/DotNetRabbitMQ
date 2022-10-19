const amqp = require("amqplib");
require('dotenv').config();

async function connect() {
  try {
    const connection = await amqp.connect(
        `amqp://${process.env.RABBITMQ_USERNAME}:${process.env.RABBITMQ_PASSWORD}@${process.env.RABBITMQ_HOST}:5672`
    );
    const channel = await connection.createChannel();
    await channel.assertQueue("hello");
    channel.consume("hello", (message) => {
      //   const input = JSON.parse(message.content.toString());
      const input = message.content.toString();
      console.log(`Received message: ${input}`);
      channel.ack(message);
    });
    console.log(`Waiting for messages...`);
  } catch (ex) {
    console.error(ex);
  }
}

connect();
