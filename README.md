# Teste de migração de dados
## Objetivo
O objetivo deste teste é simular uma situação bastante corriqueira do cotidiano do time em empresas de tecnologia:
- Baixar os dados de uma API;
- Fazer a leitura de arquivos e transformá-los em classe/objeto;
- Inserir os dados em um banco de dados (preferencialmente MongoDB);
- Criar uma API que retorne os dados inseridos no formato Json;

Para isso, criamos uma simulação com uma base pública de CNPJ's da receita federal. Todas as informações necessárias para resolver os desafios serão descritas a seguir:

## Passo a passo
### 1. Crie um robô que baixe um ou mais arquivos de CNPJ

Os arquivos completos podem ser baixados no site da [receita](https://receita.economia.gov.br/orientacao/tributaria/cadastros/cadastro-nacional-de-pessoas-juridicas-cnpj/dados-publicos-cnpj)

Exemplo:  
- [Dados abertos de CNPJ 1](http://200.152.38.155/CNPJ/DADOS_ABERTOS_CNPJ_01.zip)
- [Dados abertos de CNPJ 2](http://200.152.38.155/CNPJ/DADOS_ABERTOS_CNPJ_02.zip)
 
Você deverá baixar esses arquivos através de um robô, mas também poderá baixá-los manualmente para conseguir fazer as outras etapas do desafio caso sinta alguma dificuldade nesse processo.

Observação: Estamos disponibilizando uma versão em nosso Google Drive caso os links da receita estejam fora do ar: 
- [Cópia de um dos dados abertos no Google Drive](https://drive.google.com/file/d/11JEE8WKSD9_FBAfGfiFq_z-ZtS1bmGeR/view?usp=sharing)

### 2. Extrair o arquivo zip

Faça o seu robô extrair o arquivo zip que foi baixado no exemplo anterior.

### 3. Ler o arquivo e fazer o parser

Faça o parser apenas dos campos abaixo: 

- Dados empresas (LAYOUT PRINCIPAL)
    - CNPJ
    - IDENTIFICADOR MATRIZ/FILIAL 
    - RAZÃO SOCIAL/NOME EMPRESARIAL
    - NOME FANTASIA 
    - CAPITAL SOCIAL DA EMPRESA
    - SITUAÇÃO CADASTRAL 
    - DATA SITUACAO CADASTRAL 
    - CEP 

- Sócios (LAYOUT SOCIOS) 
    - IDENTIFICADOR DE SOCIO 
    - NOME SOCIO (NO CASO PF) OU RAZÃO SOCIAL (NO CASO PJ) 
    - CNPJ/CPF DO SÓCIO   

Para inforamações sobre o layout do arquivo acesse o [Documento com layout do arquivo](http://200.152.38.155/CNPJ/LAYOUT_DADOS_ABERTOS_CNPJ.pdf) ou a [cópia dele no Google Drive](https://drive.google.com/file/d/11Nc-60v0QNA02J6ZYfqUPglYIX_IlcEu/view?usp=sharing)

### 4. Inserir os dados em um banco de dados MongoDB

Depois de fazer o parser dos dados, faça a inserção deles em um banco de dados MongoDB. Caso prefira utilizar outro banco de dados para completar essa etapa do desafio, não tem problema.

### 5. Crie uma API que retorne os dados

Crie uma API que ao enviar o número do CNPJ retorne as informações daquele CNPJ em formato JSON.

Exemplo:
- request: http://localhost/cnpj/00000000000191 
- response: Contém os dados do CNPJ no formato JSON