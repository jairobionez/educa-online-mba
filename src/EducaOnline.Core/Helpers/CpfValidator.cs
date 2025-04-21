namespace EducaOnline.Core.Helpers
{
    public static class CpfValidator
    {
        public static bool IsValid(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            if (cpf.Length != 11)
                return false;

            // CPF com todos os dígitos iguais não é válido
            if (cpf.Distinct().Count() == 1)
                return false;

            // Valida os dígitos verificadores
            var cpfArray = cpf.Select(c => int.Parse(c.ToString())).ToArray();

            // Primeiro dígito verificador
            int sum = 0;
            for (int i = 0; i < 9; i++)
                sum += cpfArray[i] * (10 - i);

            int firstVerifier = sum % 11;
            firstVerifier = firstVerifier < 2 ? 0 : 11 - firstVerifier;

            if (cpfArray[9] != firstVerifier)
                return false;

            // Segundo dígito verificador
            sum = 0;
            for (int i = 0; i < 10; i++)
                sum += cpfArray[i] * (11 - i);

            int secondVerifier = sum % 11;
            secondVerifier = secondVerifier < 2 ? 0 : 11 - secondVerifier;

            return cpfArray[10] == secondVerifier;
        }
    }
}
