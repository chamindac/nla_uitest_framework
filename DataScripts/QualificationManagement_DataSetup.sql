Declare
--system needs--
@myappUserEmployeeId int = 3,
@myappUserDepartmentIds nvarchar(max),

--Common--
@today datetime,
@maxId int, 
@minId int,
@TestDataKey nvarchar(255),
@CreatedById int,
@CreatedOn datetime,

--Department--
@DepartmentName nvarchar(100),
@DepartmentDescription nvarchar(max),
@DepartmentGuid uniqueidentifier,
@DepartmentStatus int,

--Designation--
@DesignationName nvarchar(100),
@DesignationDescription nvarchar(max),
@DesignationStatus int,

--LeaveAct--
@LeaveActName nvarchar(100),
@LeaveActDescription nvarchar(max),
@LeaveActGuid uniqueidentifier,
@LeaveActStatus int,
@LeaveActProcessType int,

--EmployeeCategory--
@EmployeeCategoryName nvarchar(100),
@EmployeeCategoryDescription nvarchar(max),
@EmployeeCategoryStatus int,

--Salutation--
@SalutationName nvarchar(100),
@SalutationDescription nvarchar(max),
@SalutationStatus int,

--Gender--
@GenderName nvarchar(100),
@GenderDescription nvarchar(max),
@GenderStatus int,

-- Employee--
@EmployeeId int,
@CustomEmployeeId nvarchar(20),
@EmployeeGuid uniqueidentifier,
@DOB datetime,
@FirstName nvarchar(500),
@LastName nvarchar(500),
@Initials nvarchar(50),
@BasicSalary decimal(18,2),
@DOJ datetime,
@DepartmentId int,
@DesignationId int,
@LeaveActMasterId int,
@EmployeeCategoryId int,
@SalutationId int,
@GenderId int,
@NoPayLeaveAmount float,
@OvertimeApplicable bit,
@SubDepartmentId int,
@SalaryMethod int,
@MaxAdvancePercentage float,
@Amount float,
@AllowLeaveDeduction bit,
@EmployeeStatus int,
@EarlyGracePeriodMonthly int,
@LateGracePeriodMonthly int,
@IntCustomEmployeeId int,
@IsSecondaryJob bit,
@OTStartDate datetime,
@IsF1 bit,
@IsAttendanceCheck bit,

--Institute--
@InstituteId int,
@InstituteName nvarchar(100),
@InstituteDescription nvarchar(max),
@InstituteStatus int,

--Institute--
@QualificationId int,
@QualificationGuid uniqueidentifier,
@QualificationFromDate datetime,
@QualificationToDate datetime,
@QualificationRemarks nvarchar(100),
@QualificationDegree nvarchar(max),
@QualificationGrade nvarchar(max),
@QualificationStatus int

--Setting Variables--
--Common--
Set @today = Convert(date, getdate())
Set @TestDataKey = LEFT(CONVERT(nvarchar(255), newid()),8)
Set @minId = 99990000
Set @maxId = 99999999
Set @CreatedById = 69 --need fix
Set @CreatedOn = getdate()

--Department--
Set @DepartmentName = Concat('Dept',@TestDataKey)
Set @DepartmentDescription = Concat(@DepartmentName, ' Description')
Set @DepartmentGuid = newid()
Set @DepartmentStatus =1 

--Designation--
Set @DesignationName = Concat('Desig',@TestDataKey)
Set @DesignationDescription = Concat(@DesignationName, ' Description')
Set @DesignationStatus = 1 --Active

--LeaveAct--
Set @LeaveActName = Concat('LeaveAct',@TestDataKey)
Set @LeaveActDescription = Concat(@LeaveActName, ' Description')
Set @LeaveActGuid = newid()
Set @LeaveActStatus = 1 --Active
Set @LeaveActProcessType = 2 --enum in sytem JoinedYearBased = 1,ActEntitlmentBased = 2,ActEntilementAndJoinedYearBased = 3

--EmployeeCategory--
Set @EmployeeCategoryName = Concat('EmpCat',@TestDataKey)
Set @EmployeeCategoryDescription = Concat(@EmployeeCategoryName, ' Description')
Set @EmployeeCategoryStatus = 1 --Active

--Salutation--
Set @SalutationName = Concat('Salut',@TestDataKey)
Set @SalutationDescription = Concat(@SalutationName, ' Description')
Set @SalutationStatus = 1 --Active

--Gender--
Set @GenderName = Concat('Gen',@TestDataKey)
Set @GenderDescription = Concat(@GenderName, ' Description')
Set @GenderStatus = 1 --Active

--Employee--
Set @CustomEmployeeId = ABS(CHECKSUM(NEWID()) % (@maxId - @minId + 1)) + @minId
Set	@EmployeeGuid = newid()
Set @DOB = dateadd(YEAR,-30, @today)
Set @FirstName = Concat('James',@TestDataKey)
Set @LastName = Concat('Smith',@TestDataKey)
Set @Initials = 'J.'
Set @BasicSalary = 0
Set @DOJ = dateadd(YEAR,-8, @today)
--Set @DepartmentId = 2 --need fix
--Set @DesignationId = 34 --need fix
--Set @LeaveActMasterId = 5 --need fix
--Set @EmployeeCategoryId = 5 --need fix
--Set @SalutationId = 4 --need fix
--Set @GenderId = 3 --need fix
Set @NoPayLeaveAmount = 0
Set @OvertimeApplicable = 0
Set @SalaryMethod = 1 --enum in system Monthly=1,Weekly=2,Fortnightly = 3,Daily=4
Set @MaxAdvancePercentage= 0
Set @Amount = 0
Set @AllowLeaveDeduction = 0 -- bit
Set @EmployeeStatus = 1 -- Enum in system Active = 1,Deleted = 2,Hold = 3,Canceled = 4
Set @EarlyGracePeriodMonthly = 0
Set @LateGracePeriodMonthly = 0
Set @IntCustomEmployeeId = (cast (@CustomEmployeeId as int))
Set @IsSecondaryJob = 0 -- bit
Set @OTStartDate = @DOJ
Set @IsF1 = 0 --bit
Set @IsAttendanceCheck = 0 --bit

--Institute--
Set @InstituteName = Concat('Inst',@TestDataKey)
Set @InstituteDescription = Concat(@InstituteName, ' Description')
Set @InstituteStatus = 1 --Active

--Institute--
Set @QualificationGuid = newid()
Set @QualificationFromDate = dateadd(YEAR,-5, @today)
Set @QualificationToDate = dateadd(YEAR,-4, @today)
Set @QualificationRemarks = Concat('Remarks ',@TestDataKey)
Set @QualificationDegree = Concat('Degree',@TestDataKey)
Set @QualificationGrade = Concat('Grade ',@TestDataKey)
Set @QualificationStatus = 1 -- Active


--Create Test Data--

--Department--
INSERT INTO [Lookup].[Department]
           ([Value],[Description],[DepartmentGuid],[Status],[CreatedById],[CreatedOn])
     VALUES
           (@DepartmentName,@DepartmentDescription,@DepartmentGuid,@DepartmentStatus,@CreatedById,@CreatedOn)

Select @DepartmentId = [DepartmentId] from  [Lookup].[Department] where [Value] = @DepartmentName

--Add Department to Authorized Departments--
Select @myappUserDepartmentIds = [DepartmentIds] from [HumanResource].[EmployeeFilter] 
where [EmployeeId] = @myappUserEmployeeId

Update [HumanResource].[EmployeeFilter]
Set [DepartmentIds] = concat(@myappUserDepartmentIds,',',@DepartmentId)
where [EmployeeId] = @myappUserEmployeeId

--Designation--
INSERT INTO [Lookup].[Designation]
           ([Value],[Description],[Status],[CreatedById],[CreatedOn])
     VALUES
           (@DesignationName,@DesignationDescription,@DesignationStatus,@CreatedById,@CreatedOn)

Select @DesignationId = [DesignationId] from  [Lookup].[Designation] where [Value] = @DesignationName

--LeaveAct--
INSERT INTO [Lookup].[LeaveActMaster]
           ([Value],[Description],[Status],[CreatedById],[CreatedOn],[LeaveProcessType],[LeaveActMasterGuid])
     VALUES
           (@LeaveActName,@LeaveActDescription,@LeaveActStatus,@CreatedById,@CreatedOn,@LeaveActProcessType,@LeaveActGuid)

Select @LeaveActMasterId = [LeaveActMasterId] from  [Lookup].[LeaveActMaster] where [Value] = @LeaveActName

--EmployeeCategory--
INSERT INTO [Lookup].[EmployeeCategory]
           ([Value],[Description],[Status],[CreatedById],[CreatedOn])
     VALUES
           (@EmployeeCategoryName,@EmployeeCategoryDescription,@EmployeeCategoryStatus,@CreatedById,@CreatedOn)

Select @EmployeeCategoryId = [EmployeeCategoryId] from  [Lookup].[EmployeeCategory] where [Value] = @EmployeeCategoryName

--Salutation--
INSERT INTO [Lookup].[Salutation]
           ([Value],[Description],[Status],[CreatedById],[CreatedOn])
     VALUES
           (@SalutationName,@SalutationDescription,@SalutationStatus,@CreatedById,@CreatedOn)

Select @SalutationId = [SalutationId] from  [Lookup].[Salutation] where [Value] = @SalutationName

--Gender--
INSERT INTO [Lookup].[Gender]
           ([Value],[Description],[Status],[CreatedById],[CreatedOn])
     VALUES
           (@GenderName,@GenderDescription,@GenderStatus,@CreatedById,@CreatedOn)

Select @GenderId = [GenderId] from  [Lookup].[Gender] where [Value] = @GenderName

--Employee--
INSERT INTO [HumanResource].[Employee]
           ([CustomEmployeeId],[EmployeeGuid],[DOB],[FirstName],[LastName],[Initials],[BasicSalary],[DOJ],[DepartmentId],[DesignationId]
		   ,[LeaveActMasterId],[EmployeeCategoryId],[SalutationId],[GenderId],[NoPayLeaveAmount],[OvertimeApplicable],[SalaryMethod]
           ,[MaxAdvancePercentage],[Amount],[AllowLeaveDeduction],[Status],[CreatedById],[CreatedOn],[EarlyGracePeriodMonthly]
           ,[LateGracePeriodMonthly],[IntCustomEmployeeId],[IsSecondaryJob],[OTStartDate],[IsF1],[IsAttendanceCheck])
     VALUES
           (@CustomEmployeeId,@EmployeeGuid,@DOB,@FirstName,@LastName,@Initials,@BasicSalary,@DOJ,@DepartmentId,@DesignationId,
           @LeaveActMasterId,@EmployeeCategoryId,@SalutationId,@GenderId,@NoPayLeaveAmount,@OvertimeApplicable,@SalaryMethod,
           @MaxAdvancePercentage,@Amount,@AllowLeaveDeduction,@EmployeeStatus,@CreatedById,@CreatedOn,@EarlyGracePeriodMonthly,
           @LateGracePeriodMonthly,@IntCustomEmployeeId,@IsSecondaryJob,@OTStartDate,@IsF1,@IsAttendanceCheck)

Select @EmployeeId = [EmployeeId] from  [HumanResource].[Employee] where [EmployeeGuid] = @EmployeeGuid

--Institute--
INSERT INTO [Lookup].[Institute]
           ([Value],[Description],[Status],[CreatedById],[CreatedOn])
     VALUES
           (@InstituteName,@InstituteDescription,@InstituteStatus,@CreatedById,@CreatedOn)

Select @InstituteId = [InstituteId] from  [Lookup].[Institute] where [Value] = @InstituteName

--Qualification--
INSERT INTO [HumanResource].[Qualification]
           ([QualificationGuid],[EmployeeGuid],[EmployeeId],[FromDate],[ToDate]
           ,[Remarks],[Degree],[Grade],[Status],[CreatedById],[CreatedOn]
		   ,[InstituteId])
     VALUES
           (@QualificationGuid,@EmployeeGuid,@EmployeeId,@QualificationFromDate,@QualificationToDate
		   ,@QualificationRemarks,@QualificationDegree,@QualificationGrade,@QualificationStatus,@CreatedById,@CreatedOn
		   ,@InstituteId)

Select @QualificationId = [QualificationId] from [HumanResource].[Qualification] 
where [EmployeeGuid] = @EmployeeGuid and [InstituteId] = @InstituteId and [Degree] = @QualificationDegree

select @CustomEmployeeId as CustomEmployeeId, @EmployeeGuid as EmployeeGuid,  @InstituteName as InstituteName
	, @TestDataKey as TestDataKey, @InstituteName as InstituteName, @QualificationDegree as QualificationDegree