// See https://aka.ms/new-console-template for more information

class Program
{
    static void solve(int n, char from, char to, char aux)
    {
        if (n == 0)
            return;
        solve(n - 1, from, aux, to);
        Console.WriteLine("หยิบแผ่น " + n + " จาก " + from + " ไป " + to);
        solve(n - 1, aux, to, from);
    }

    static int S(int n)
    {
        if (n == 0)
            return 0;
        return n + S(n - 1);
    }

    static int A(int n)
    {
        if (n == 1)
            return 1;
        return n * A(n - 1);
    }

    static void Main()
    {
        //solve(125, 'A', 'C', 'B'); //ตัวแรก
        //Console.WriteLine(S(100)); //ตัวสอง
        //Console.WriteLine(A(4));
    }
}
