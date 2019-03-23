using UnityEngine;

public class Bullet : Projectile
{
    public LineRenderer LineRenderer;
    public float LifeTime;
    public float Speed;
    public float Length;

    private Vector3 end;

    public void SetStart(Vector3 start)
    {
        this.LineRenderer.SetPosition(0, start);
    }

    public void SetEnd(Vector3 end)
    {
        this.end = end;
    }

    public override void SetSource(Vector3 source)
    {
        base.SetSource(source);
        this.LineRenderer.SetPosition(0, source);
    }

    protected override void Update()
    {
        this.LifeTime -= Time.deltaTime;
        if (this.LifeTime <= 0)
        {
            Destroy(this.gameObject);
        }

        Vector3 currentStart = this.LineRenderer.GetPosition(0);
        Vector3 nextStart = Vector3.MoveTowards(currentStart, this.target, Speed * Time.deltaTime);
        Vector3 nextEnd = Vector3.MoveTowards(nextStart, this.target, Length);

        if (Vector3.Distance(nextStart, nextEnd) < 0.9*Length)
        {
            Destroy(this.gameObject);
        }

        this.LineRenderer.SetPosition(0, nextStart);
        this.LineRenderer.SetPosition(1, nextEnd);
        this.LineRenderer.enabled = true;
    }
}

