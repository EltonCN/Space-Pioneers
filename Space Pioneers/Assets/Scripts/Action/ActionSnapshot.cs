public interface ActionSnapshot
{
    public void undo();
    
    public string getActionMessage();

    public int getActionCost();
}