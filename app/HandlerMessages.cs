using System.Net.Sockets;
using System.Text;

class HandlerMessages
{
    private String data;
    private byte[] bytes = new byte[1024];

    public void listening(Socket connection)
    {
        while(true)
        {
            int bytesReciev = connection.Receive(bytes);
            
            if(bytesReciev != 0){
                data += Encoding.ASCII.GetString(bytes, 0, bytesReciev);
                Console.WriteLine("Text received : {0}", data);

                byte[] msg = Encoding.ASCII.GetBytes(data);
                connection.Send(msg);
            }

            /*if(data.IndexOf("<EOF>") > -1)
            {
                connection.Shutdown(SocketShutdown.Both);
                connection.Close();
                break;
            }*/
        }
        
    }
}