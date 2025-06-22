﻿namespace Nike.Chatbot.SemanticKernel.DTOs;

public class ServiceResponse<T> where T : class
{
    public bool IsSuccess { get; set; }
    
    public string? Message { get; set; }
    
    public List<T>? ObjectsList { get; set; }
}