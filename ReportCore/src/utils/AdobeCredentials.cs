using Adobe.DocumentServices.PDFTools.auth;

internal static class AdobeCredentials {
    internal static string PrivateKey = @"-----BEGIN RSA PRIVATE KEY-----
MIIEpAIBAAKCAQEAn2HKJUyGrHvIMHgzc60r+JK+0YpLBBHS8mphdx0zRoLPD17P
f62HeunMN6Nf34Dy/28Y/+iEm1xgjkUgLpsxWOvi9/5KW9j79G/W2mWloEp1nXC5
h3fUAx1CKUh6fCIpNWqBSw5cbWWz7eKNurI/vg56JE8zU4H0XNdfsSq+bYLPJ5rN
sfuWm3wUIueXLsPF/rctRE0Pn13d9CWX7JRxl55xUa6h8vIGDLK4KNZJWxxLJDrJ
9PWWh3k28fJUcjnURkSDiFSp/4y+nV98qfjdlq4vPPAQrMuSDrgOOaxQolZd2Pei
9IACOx8WAQYbFwsnAqZcW/qUR2Fb5EOTZpA5IwIDAQABAoIBAQCD+VqEliByFLFI
iU+5emyNkR2Wrte9D/FHsBTyp+g8e7Zu+MgC5jLn8U6bx5lsnf8Yyeq+hM/BQbXm
OJEUZodvJSw0w4jZEwxWdIFJKKAD4YQQkTXfWbbhuUl+m7hor3TF+WIEP2kDYRKj
aVSIgm7LtnsnM7nBbTtl4SUos8cpL7cW++ioVixhLYZkJxBQkdUkmmaeJ8ZyklHU
lm1D5hx4FJ2wEgM1kMegP6J6yAYyPZuiW/a0MsgjYuhwhrbk5PhFRdTiG1e5prk7
xSzTpxR0G9MywDC5MZrMfA+b15RsmgE4xiBZZotjqW23GH7TNGeJNj2/7YhOg+Pr
hzxizeTpAoGBANvQu+mPHF7ijLDQ/g2OevnH6OHREo1R0jWlLZLMb/tQkKz9V6ZR
eh/nBXeLeci3QfPBNl7UUEhXoAIHEcfS002h7WYePFB1CUwDZAsdDzVpag3YSb5G
Ul+PXkZY7dSyFs2T09K1W3bZ6MX/l0r3ZhDXL/asMynQCuS0+GbElzIXAoGBALme
U0uKcsU0mkTaCS4I1L70wmuBzbLu4KY0dxWVRPVkYuJj6whx+mozn/vMvtKvIOGn
hoUQJ/PhcKkAaVL2qfS2V2qEI48j75Sv7PomFAYHZyCj78xQu+k2ROfgaIM/5lup
ma/b7miLnPdnxEr9eIgmKUIaNQ6UgmMqsYTizFTVAoGBAKKHJrndh+OcbNw72uyZ
IdJX/pghJy94hQ4HMLgvEslmW2Kc+1bNPZgjD8bBSO2gmlIp1RKutyGWMIfvRDVU
a8ol5UsxJfVzY8lhZHJXLSyX4cOU3Ymjozpm3hTzof43I2cjW2abk6JAIr1raykR
3dSr1xnzXu0Wl0ddi6Eo0IZHAoGAYiHqN43Cj6/3v17ma4gPscUE5IGDlo3kBvrF
2otIIdQo0zewEo7vrSYN9rmQSSJ9Z8+BtueLt8wAG9kz1cDCqbWdEZs6kXqWNy2r
Q7TN8UIIq6EALiygq/MdCtoBZyJpTxyjO+4yZOMq4UHi7SKEjrZeKaxhUIwDQxpX
L8IX9OECgYBH9jJo7DAMfS0crCMO+gq8i/JE0s7/TsWjlap4t85uGsJF9SpXp7re
vYdszEWAVuWhx64GBnXeZza4M8cBIsHexwNziiSibR1joLDffCKaW/Lr6QezzMIx
NzQa7VKWh4qLEoyepofRGlHQ/VOLRu/LG5Lv1urTZK2HTotVGZLXsQ==
-----END RSA PRIVATE KEY-----
";

    internal static Credentials GetAdobeCredentials() {
        return Credentials.ServiceAccountCredentialsBuilder()
                                        .WithAccountId("C65C52D6609989C10A495FF8@techacct.adobe.com")
                                        .WithClientId("486ae3f491d3498a88f484925c5202ff")
                                        .WithClientSecret("p8e-i-JAYXk-Rxn2KwzJHeQ4_n1H7TcpM9oi")
                                        .WithOrganizationId("EC0313956099898D0A495E3D@AdobeOrg")
                                        .WithPrivateKey(AdobeCredentials.PrivateKey)
                                        .Build();
    }
}