using UnityEngine;

public class scr_event_setCutsceneStep : scr_event_template
{
    public Cutscene the_cutscene;
    public int cutscene_step_to_go = 0;
    public bool reactivates_on_end = true;
    bool already_seen = false;

    protected override void Start()
    {
        base.Start();
        the_cutscene.finished_a_cutscene.AddListener(on_cutscene_end);
    }

    public void on_cutscene_end()
    {
        activated = false;

        if(!reactivates_on_end)
        {
            already_seen = true;
        }
    }

    public override void event_behavior()
    {
        if(!already_seen)
        {
            the_cutscene.jump_to_step(cutscene_step_to_go);
        }
    }
}
