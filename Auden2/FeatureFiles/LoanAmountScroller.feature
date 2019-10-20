Feature: LoanAmountScroller
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario: Loan Amount Scroller Functions 
	Given  Slider Default Loan Amount Amount
	When Move slider in any direction
	And Minimum amount should be
	And Maximum amount should be