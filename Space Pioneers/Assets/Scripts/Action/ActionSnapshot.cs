public interface ActionSnapshot
{
    public void undo();
    
    public string getActionMessage();

    public float getActionCost();
}