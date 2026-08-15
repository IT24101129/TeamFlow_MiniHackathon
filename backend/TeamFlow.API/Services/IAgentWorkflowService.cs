namespace TeamFlow.API.Services
{
    /// <summary>
    /// Architectural Extension Point: Agentic AI Workflow Service.
    /// Note for SE3090 evaluation: This interface acts as the clean integration boundary for 
    /// Component 4 (AI Workflow & Approval) in future coursework iterations.
    /// No AI engines, LLM calls, or third-party AI frameworks are active in this MVP stage.
    /// </summary>
    public interface IAgentWorkflowService
    {
        Task<bool> IsWorkflowEnabledAsync();
    }

    public class AgentWorkflowService : IAgentWorkflowService
    {
        public Task<bool> IsWorkflowEnabledAsync()
        {
            // Placeholder extension return value indicating AI subsystem will be plugged here
            return Task.FromResult(false);
        }
    }
}
