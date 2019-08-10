using Sansar.Simulation;
using System.Collections.Generic;
using Sansar.Utility;

public class VisitorPrinter : SceneObjectScript {
  string url = "https://darwinrecreant.github.io/serverless-media-demo/index.html";
  public override void Init() {
    ScenePrivate.User.Subscribe(User.AddUser, (UserData data) => {
      Update();
    });
    ScenePrivate.User.Subscribe(User.RemoveUser, (UserData data) => {
      Update();
    });
    Update();
  }

  void Update() {
    List<string> names = new List<string>();
    foreach (AgentPrivate agent in ScenePrivate.GetAgents()) {
      names.Add(agent.AgentInfo.Handle);
    }

    JsonSerializer.Serialize(names.ToArray(), (JsonSerializationData<string[]> data) => {
      if (data.Success) {
        ScenePrivate.OverrideMediaSource(url + "#" + data.JsonString);
      }
    });
  }
}
