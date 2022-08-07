namespace NerdStore.Pagamentos.AntiCorruption
{
    public interface IPayPalGateway
    {
        // Primeiro você precisa pegar uma chave de serviço do PayPal,
        // onde você passa a chave segredo do seu registro no PayPal e a encriptionKey
        string GetPayPalServiceKey(string apiKey, string encriptionKey);

        // Com a chave de serviço, você gera uma CardHashKey,
        // você recebe um dado encriptado que vai ser utilizado para aquele pagamento
        string GetCardHashKey(string serviceKey, string cartaoCredito);

        bool CommitTransaction(string cardHashKey, string orderId, decimal amount);
    }
}
