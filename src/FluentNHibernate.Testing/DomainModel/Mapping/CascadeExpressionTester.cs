using System;
using FluentNHibernate.Mapping;
using NUnit.Framework;
using Rhino.Mocks;

namespace FluentNHibernate.Testing.DomainModel.Mapping
{
	[TestFixture]
	public class CascadeExpressionTester
	{
		#region Test Setup
		public IMappingPart _mockPart;
		public CascadeExpression<IMappingPart> _cascade;
		public Func<IMappingPart> _currentCascadeAction;
		
		[SetUp]
		public virtual void SetUp()
		{
            _mockPart = MockRepository.GenerateStub<IMappingPart>();
			_cascade = new CascadeExpression<IMappingPart>(_mockPart);
		}

		protected CascadeExpressionTester A_call_to(Func<IMappingPart> cascadeAction)
		{
			_currentCascadeAction = cascadeAction;
			return this;
		}

		public void should_set_the_cascade_value_to(string expected)
		{
			_currentCascadeAction();
            _mockPart.AssertWasCalled(p => p.SetAttribute("cascade", expected));
		}

		#endregion

		[Test]
		public void All_should_add_the_correct_cascade_attribute_to_the_parent_part()
		{
			A_call_to(_cascade.All).should_set_the_cascade_value_to("all");
		}

		[Test]
		public void None_should_add_the_correct_cascade_attribute_to_the_parent_part()
		{
			A_call_to(_cascade.None).should_set_the_cascade_value_to("none");
		}

		[Test]
		public void SaveUpdate_should_add_the_correct_cascade_attribute_to_the_parent_part()
		{
			A_call_to(_cascade.SaveUpdate).should_set_the_cascade_value_to("save-update");
		}

		[Test]
		public void Delete_should_add_the_correct_cascade_attribute_to_the_parent_part()
		{
			A_call_to(_cascade.Delete).should_set_the_cascade_value_to("delete");
		}
	}
}
