using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Transactions;
using Experimental.System.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using UserDataCommonLayer.Models;
using UserDataRepositoryLayer.Interfaces;

namespace UserDataRepositoryLayer.Services
{
    
    public class MSMQ_Service
    {
        
        private readonly string _queuePath;
        public IEmailService _emailService;
        public MSMQ_Service(IConfiguration config,  IEmailService emailService)
        {
            _queuePath = @".\Private$\" + config.GetSection("MessageQueueName").Value;
            _emailService = emailService;
        }
        

        public void SendToken(TokenMessage tokenMessage)
        {
            MessageQueue messageQueue;
            if (!MessageQueue.Exists(_queuePath))
            {
                messageQueue = MessageQueue.Create(_queuePath);
            }
            else
            {
                messageQueue = new MessageQueue(_queuePath);
            }
            messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(TokenMessage) });
            Message mqMessage = new Message();
            mqMessage.Formatter = new XmlMessageFormatter(new Type[] { typeof(TokenMessage) });
            mqMessage.Body = tokenMessage;
            mqMessage.Label = "TokenMessage for Forget Password";
            messageQueue.Send(mqMessage);
            Message msg = messageQueue.Receive();
            msg.Formatter = new XmlMessageFormatter(new Type[] { typeof(TokenMessage) });
            ComposeMail composeMail = new ComposeMail()
            {
                ToEmail = "testemail.4294@gmail.com",
                Subject = "Send token to Reset Password",
                Body = msg.Body.ToString()
            };

            _emailService.SendEmailAsync(composeMail);
            // Add an event handler for the ReceiveCompleted event.
            messageQueue.ReceiveCompleted += new ReceiveCompletedEventHandler(MyReceiveCompleted);

            // Begin the asynchronous receive operation.
            messageQueue.BeginReceive();

            messageQueue.Close();

        }

        public void MyReceiveCompleted(Object source, ReceiveCompletedEventArgs asyncResult)
        {
            MessageQueue messageQueue = (MessageQueue)source;
            messageQueue.Formatter = new XmlMessageFormatter(new Type[] {typeof(TokenMessage)});
            Message message = messageQueue.EndReceive(asyncResult.AsyncResult);

            //ComposeMail composeMail = new ComposeMail()
            //{
            //    ToEmail = "testeremail129@gmail.com",
            //    Subject = "Send token to Reset Password",
            //    Body = message.Body.ToString()
            //};

            //_emailService.SendEmailAsync(composeMail);

            messageQueue.BeginReceive();
            return;
        }
    }
}
