// See https://aka.ms/new-console-template for more information
using Day1;

public class Dial
{
    private uint actualState;
    private uint numberOfZeros;

    public uint NumberOfZeros
    {
        get { return this.numberOfZeros; }
	}

	public Dial(uint initialState)
    {
        this.actualState = initialState;
        this.numberOfZeros = 0;
	}

    public void ApplyIstruction(Istruction instruction)
    {
        this.actualState = instruction.ApplyRotation(this.actualState);

        if (this.actualState == 0)
        {
            this.numberOfZeros++;
		}
	}


}