Declare
--system needs--
@myappUserEmployeeId int = 3,
@myappUserDepartmentIds nvarchar(max),

--Common--
@TestDataKey nvarchar(255) = '{{TestDataKey}}',

--Department--
@DepartmentId int


Set @TestDataKey = concat('%', @TestDataKey,'%')

--Qualifications--
DELETE FROM [HumanResource].[Qualification]
	WHERE [Degree] like @TestDataKey 

--Institute--
DELETE FROM [Lookup].[Institute]
	WHERE [Value] like @TestDataKey 

--Employee--
DELETE FROM [HumanResource].[Employee]
      WHERE [FirstName] like @TestDataKey 
	  and [LastName] like @TestDataKey

--Gender--
DELETE FROM [Lookup].[Gender]
	WHERE [Value] like @TestDataKey

--Salutation--
DELETE FROM [Lookup].[Salutation]
	WHERE [Value] like @TestDataKey

--EmployeeCategory--
DELETE FROM [Lookup].[EmployeeCategory]
	WHERE [Value] like @TestDataKey

--LeaveAct--
DELETE FROM [Lookup].[LeaveActMaster]
	WHERE [Value] like @TestDataKey

--Designation--
DELETE FROM [Lookup].[Designation]
	WHERE [Value] like @TestDataKey

--Department--
Select @DepartmentId = [DepartmentId] from  [Lookup].[Department] where [Value] like  @TestDataKey

--Department Auth--
Select @myappUserDepartmentIds = [DepartmentIds] from [HumanResource].[EmployeeFilter] 
where [EmployeeId] = @myappUserEmployeeId

--select @myappUserDepartmentIds
Set @myappUserDepartmentIds = concat(','+ @myappUserDepartmentIds, ',')
Set @myappUserDepartmentIds = replace(@myappUserDepartmentIds,concat(',',CAST(@DepartmentId as varchar),','),',')

Set @myappUserDepartmentIds = Left(@myappUserDepartmentIds, (len(@myappUserDepartmentIds)-1))
Set @myappUserDepartmentIds = right(@myappUserDepartmentIds, (len(@myappUserDepartmentIds)-1))

--select @myappUserDepartmentIds

Update [HumanResource].[EmployeeFilter]
Set [DepartmentIds] = @myappUserDepartmentIds
where [EmployeeId] = @myappUserEmployeeId

--Department--
DELETE FROM [Lookup].[Department]
      WHERE [DepartmentId] = @DepartmentId