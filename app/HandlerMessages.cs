using System.Net.Sockets;
using System.Text;

class HandlerMessages
{
    private int connections;
    private String data;
    private byte[] bytes = new byte[1024];

    public void listening(Socket connection)
    {
        this.connections++;
        while(true)
        {
            int bytesReciev = connection.Receive(bytes);
            
            if(bytesReciev != 0){
                data += Encoding.ASCII.GetString(bytes, 0, bytesReciev);
                Console.WriteLine("Text received : {0}", data);

                String quantityConnections = ""+this.connections;

                byte[] msg = Encoding.ASCII.GetBytes(quantityConnections);
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