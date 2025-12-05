using Day1;

public class Dial
{
    private uint actualState;
    private uint numberOfZeros;
    private uint numberOfClicks;

	public uint NumberOfZeros
    {
        get { return this.numberOfZeros; }
	}

	public uint NumberOfClicks
	{
		get { return this.numberOfClicks; }
	}

	public Dial(uint initialState)
    {
        this.actualState = initialState;
        this.numberOfZeros = 0;
	}

    public void ApplyIstruction(Istruction instruction)
    {
		uint currentState = this.actualState;
		uint currentClicks = 0;


		switch (instruction.Rotation)
		{
			case RotationSense.Right:

				currentClicks = ((currentState + instruction.Steps) / 100u);
				currentState = (currentState + instruction.Steps) % 100u;

				break;
			case RotationSense.Left:
				currentState = (currentState + 100u - (instruction.Steps % 100u)) % 100u;
				
				currentClicks = (instruction.Steps / 100u);

				currentClicks += (instruction.Steps % 100u > this.actualState) ? 1u : 0u;

				break;
			default:
				throw new InvalidOperationException("Unknown rotation sense");
		}

		if (this.actualState == 0)
        {
            this.numberOfZeros++;
		}

		this.numberOfClicks += currentClicks;

		this.actualState = currentState;
	}




}