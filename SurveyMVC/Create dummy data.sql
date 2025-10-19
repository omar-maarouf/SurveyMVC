DBCC CHECKIDENT ('Surveys', RESEED, 0);
INSERT INTO Surveys (AdminId, Title) VALUES
('28a24607-1592-478c-9520-f2ff8e24aa12', 'Customer Satisfaction Survey'),
('28a24607-1592-478c-9520-f2ff8e24aa12', 'Employee Engagement Survey'),
('28a24607-1592-478c-9520-f2ff8e24aa12', 'Product Feedback Survey'),
('28a24607-1592-478c-9520-f2ff8e24aa12', 'Website Usability Survey'),
('28a24607-1592-478c-9520-f2ff8e24aa12', 'Event Evaluation Survey'),
('28a24607-1592-478c-9520-f2ff8e24aa12', 'Training Course Review'),
('28a24607-1592-478c-9520-f2ff8e24aa12', 'Market Research Survey'),
('28a24607-1592-478c-9520-f2ff8e24aa12', 'Service Quality Survey'),
('28a24607-1592-478c-9520-f2ff8e24aa12', 'Mobile App Feedback'),
('28a24607-1592-478c-9520-f2ff8e24aa12', 'Community Opinion Survey');
GO

DBCC CHECKIDENT ('Questions', RESEED, 0);
INSERT INTO Questions (QuestionDetails, SurveyId) VALUES
('How satisfied are you with our service?', 1),
('Would you recommend our company?', 1),
('How do you feel about team collaboration?', 2),
('Is the product easy to use?', 3),
('How would you rate website navigation?', 4),
('Was the event well organized?', 5),
('Were the training materials helpful?', 6),
('What is your preferred product brand?', 7),
('Was our staff polite and helpful?', 8),
('Would you like new app features added?', 9);
GO

DBCC CHECKIDENT ('Responses', RESEED, 0);

INSERT INTO Responses (SurveyId, EmployeeId) VALUES
(1, '55a79fd5-7a25-4f84-86db-f56ca89e9206'),
(1, '55a79fd5-7a25-4f84-86db-f56ca89e9206'),
(2, '55a79fd5-7a25-4f84-86db-f56ca89e9206'),
(3, '55a79fd5-7a25-4f84-86db-f56ca89e9206'),
(4, '55a79fd5-7a25-4f84-86db-f56ca89e9206'),
(5, '55a79fd5-7a25-4f84-86db-f56ca89e9206'),
(6, '55a79fd5-7a25-4f84-86db-f56ca89e9206'),
(7, '55a79fd5-7a25-4f84-86db-f56ca89e9206'),
(8, '55a79fd5-7a25-4f84-86db-f56ca89e9206'),
(9, '55a79fd5-7a25-4f84-86db-f56ca89e9206');
GO

-- ===============================
-- Insert Answers (10)
-- ===============================
DBCC CHECKIDENT ('Answers', RESEED, 0);

INSERT INTO Answers (AnswerDetails, ResponseId, QuestionId) VALUES
('Very satisfied', 1, 1),
('Yes, definitely', 2, 2),
('Team collaboration is great', 3, 3),
('Yes, it’s easy to use', 4, 4),
('Navigation is simple and clear', 5, 5),
('Yes, very well organized', 6, 6),
('Materials were clear and helpful', 7, 7),
('Brand A and Brand B', 8, 8),
('Yes, very polite staff', 9, 9),
('Yes, more features would be great', 10, 10);
GO