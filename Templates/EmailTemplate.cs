namespace TeloApi.Templates;

public static class EmailTemplate
    {
        public static string VerificationCodeTemplate => @"
            <!DOCTYPE html>
            <html lang='es'>
            <head>
                <meta charset='UTF-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <title>Verificación de Cuenta</title>
                <style>
                    body {
                        font-family: Arial, sans-serif;
                        background-color: #f4f4f4;
                        text-align: center;
                        padding: 20px;
                    }
                    .container {
                        max-width: 400px;
                        background: white;
                        padding: 20px;
                        border-radius: 10px;
                        box-shadow: 0px 0px 10px rgba(0,0,0,0.1);
                        margin: auto;
                    }
                    h2 {
                        color: #333;
                    }
                    p {
                        color: #555;
                    }
                    .code {
                        background: #007bff;
                        color: white;
                        padding: 10px;
                        display: inline-block;
                        border-radius: 5px;
                        font-size: 20px;
                        font-weight: bold;
                    }
                    .footer {
                        font-size: 12px;
                        color: #777;
                    }
                </style>
            </head>
            <body>
                <div class='container'>
                    <h2>Verifica tu cuenta</h2>
                    <p>Usa el siguiente código para verificar tu cuenta:</p>
                    <div class='code'>{{CODE}}</div>
                    <p class='footer'>Este código expira en 10 minutos.</p>
                    <hr>
                    <p class='footer'>Si no solicitaste este código, ignora este mensaje.</p>
                    <p class='footer'><strong>HotelApp</strong></p>
                </div>
            </body>
            </html>";
    }