// Um if gigante para cada tipo
static double Desconto(tipo, valor)
{
    if (tipo == "comum")
        return 0;
    else if (tipo == "premium")
        return valor * 0.10;
    else if (tipo == "vip")
        return valor * 0.25;
    // ... e cresce pra sempre 😱
    return 0;
}