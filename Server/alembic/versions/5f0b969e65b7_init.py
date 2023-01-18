"""init

Revision ID: 5f0b969e65b7
Revises: 
Create Date: 2022-05-09 19:08:16.817462

"""
from alembic import op
import sqlalchemy as sa


# revision identifiers, used by Alembic.
revision = '5f0b969e65b7'
down_revision = None
branch_labels = None
depends_on = None


def upgrade():
    op.create_table(
    'account',
    sa.Column('id', sa.String, primary_key=True))
    
    op.create_table(
    'auth',
    sa.Column('login', sa.String, primary_key=True),
    sa.Column('hash', sa.String, nullable=False),
    sa.Column(
        'innerId',
        sa.String,
        sa.ForeignKey("account.id"),
        nullable=False)
    )


def downgrade():
    op.drop_table('account')
    op.drop_table('auth')
